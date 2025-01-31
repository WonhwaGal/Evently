using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Application.Caching;
using Evently.Common.Application.Data;
using Evently.Common.Application.EventBus;
using Evently.Common.Infrastructure.Authentication;
using Evently.Common.Infrastructure.Caching;
using Evently.Common.Infrastructure.Data;
using Evently.Common.Infrastructure.Interceptors;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace Evently.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
        IConfiguration configuration)
    {
        // Аутентификация
        services.AddAuthenticationInternal();

        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddSingleton<IDbConnectionFactory>(_ => 
            new SqlConnectionFactory(databaseConnectionString));

        services.TryAddSingleton<PublishDomainEventsInterceptor>();

        services.TryAddSingleton<IEventBus, EventBus.EventBus>();

        // Регистрация сервиса массовой пересылки сообщений
        services.AddMassTransit(configure =>
        {
            // Регистрация потребителей сообщений
            foreach (Action<IRegistrationConfigurator> configureConsumer in moduleConfigureConsumers)
            {
                configureConsumer(configure);
            }

            // Форматирование названий конечных точек в стиле kebab-case
            // (это сделает конечные точки более удобочитаемыми)
            configure.SetKebabCaseEndpointNameFormatter();

            // Конфигурация шины сообщений, делегат для настройки транспорта
            // для использования в памяти
            configure.UsingInMemory((context, cfg) =>
            {
                // Получение сообщений потребителями и регистрация требуемой топологии
                // брокера сообщений
                cfg.ConfigureEndpoints(context);
            });
        });

        AddCaching(services, configuration);

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
}
