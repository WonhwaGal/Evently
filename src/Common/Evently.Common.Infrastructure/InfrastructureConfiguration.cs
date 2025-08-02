using System.Security.Policy;
using Evently.Common.Application.Caching;
using Evently.Common.Application.Clock;
using Evently.Common.Application.Data;
using Evently.Common.Application.EventBus;
using Evently.Common.Infrastructure.Authentication;
using Evently.Common.Infrastructure.Caching;
using Evently.Common.Infrastructure.Clock;
using Evently.Common.Infrastructure.Data;
using Evently.Common.Infrastructure.EventBus;
using Evently.Common.Infrastructure.Outbox;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Core.Extensions.DiagnosticSources;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Quartz;
using StackExchange.Redis;

namespace Evently.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ILoggingBuilder loggingBuilder,
        string serviceName,
        Action<IRegistrationConfigurator, string>[] moduleConfigureConsumers,
        RabbitMqSettings rabbitMqSettings,
        IConfiguration configuration)
    {
        // Регистрация сервиса поставщика текущего времени
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        // Аутентификация
        services.AddAuthenticationInternal();

        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddSingleton<IDbConnectionFactory>(_ => 
            new SqlConnectionFactory(databaseConnectionString));
        
        if (serviceName != "Evently.Ticketing.Api")
        {
            string mongoConnectionString = configuration.GetConnectionString("Mongo") ??
                                           throw new ArgumentNullException(nameof(configuration));

            var mongoClientSettings = MongoClientSettings.FromConnectionString(mongoConnectionString);
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            mongoClientSettings.ClusterConfigurator = c => c.Subscribe(
                new DiagnosticsActivityEventSubscriber(
                    new InstrumentationOptions
                    {
                        CaptureCommandText = true
                    }));
#pragma warning disable CA2000
            services.AddSingleton<IMongoClient>(new MongoClient(mongoClientSettings));
#pragma warning restore CA2000
        }


        //services.TryAddSingleton<PublishDomainEventsInterceptor>();
        services.TryAddSingleton<InsertOutboxMessagesInterceptor>();

        services.TryAddSingleton<IEventBus, EventBus.EventBus>();

        // Регистрация сервиса массовой пересылки сообщений
        services.AddMassTransit(configure =>
        {
            // Регистрация потребителей сообщений
            //foreach (Action<IRegistrationConfigurator> configureConsumer in moduleConfigureConsumers)
            //{
            //    configureConsumer(configure);
            //}
            string instanceId = serviceName.ToLowerInvariant().Replace('.', '-');
            foreach (Action<IRegistrationConfigurator, string> configureConsumer in moduleConfigureConsumers)
            {
                configureConsumer(configure, instanceId);
            }

            configure.SetKebabCaseEndpointNameFormatter();

            // Конфигурация шины сообщений, делегат для настройки транспорта
            // для использования в памяти
            //configure.UsingInMemory((context, cfg) =>
            //{
            //    // Получение сообщений потребителями и регистрация требуемой топологии
            //    // брокера сообщений
            //    cfg.ConfigureEndpoints(context);
            //});
            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(rabbitMqSettings.Host), h =>
                {
                    h.Username(rabbitMqSettings.userName);
                    h.Password(rabbitMqSettings.Password);
                });

                // Получение сообщений потребителями и регистрация требуемой топологии
                // брокера сообщений
                cfg.ConfigureEndpoints(context);
            });
        });

        #region [!] Outbox pattern
        services.AddQuartz();
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        #endregion

        AddCaching(services, configuration);

        AddOpenTelemetry(serviceName, services, loggingBuilder);

        return services;
    }

    /// <summary>
    /// Добавить сервисы отвечающие за кеширование данных
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="ArgumentNullException"></exception>
    private static void AddCaching(IServiceCollection services, IConfiguration configuration)
    {
        string redisConnectionString = configuration.GetConnectionString("Cache") ??
                                       throw new ArgumentNullException(nameof(configuration));
        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.TryAddSingleton(connectionMultiplexer);

            services.AddStackExchangeRedisCache(optons =>
                optons.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));
        }
        catch
        {
            services.AddDistributedMemoryCache();
        }

        // Регистрация сервиса кэширования
        services.TryAddSingleton<ICacheService, CacheService>();
    }

    /// <summary>
    /// Добавить сервисы OpenTelemetry
    /// </summary>
    /// <param name="serviceName"></param>
    /// <param name="services"></param>
    /// <param name="loggingBuilder"></param>
    private static void AddOpenTelemetry(string serviceName, IServiceCollection services,
        ILoggingBuilder loggingBuilder)
    {
        // Зарегистрировать сервис OpenTelemetry
        services.AddOpenTelemetry()
            // Добавить ресурс по наименованию приложения
            .ConfigureResource(resource => resource.AddService(serviceName))
            // Настроить распределенную трассировку
            .WithTracing(tracing =>
            {
                tracing
                    // Добавить инструментарии для HttpClient и ASP.NET Core
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation(
                        options => options.SetDbStatementForStoredProcedure = false)
                    .AddRedisInstrumentation()
                    .AddSource(MassTransit.Logging.DiagnosticHeaders.DefaultListenerName)
                    .AddSqlClientInstrumentation(
                        options => options.SetDbStatementForText = true);
                tracing.AddOtlpExporter();
            });

        // Настроить ведение журнала OpenTelemetry
        loggingBuilder.AddOpenTelemetry(options =>
        {
            options.IncludeScopes = true;           // Включить области
            options.IncludeFormattedMessage = true; // Включить форматированные сообщения
        });
    }
}
