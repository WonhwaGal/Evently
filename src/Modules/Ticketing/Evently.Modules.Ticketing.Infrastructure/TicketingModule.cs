using Evently.Modules.Ticketing.Application.Abstractions.Data;
using Evently.Modules.Ticketing.Application.Carts;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.Infrastructure.Customers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Evently.Modules.Ticketing.Infrastructure.Database;
using Evently.Modules.Ticketing.Domain.TicketTypes;
using Evently.Modules.Ticketing.Infrastructure.TicketTypes;
using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.Infrastructure.Events;
using Evently.Common.Presentation.Endpoints;
using MassTransit;
using Evently.Common.Infrastructure.Outbox;
using Evently.Modules.Ticketing.Infrastructure.Outbox;
using Evently.Common.Application.Messaging;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Evently.Modules.Ticketing.Infrastructure.Inbox;
using Evently.Modules.Users.IntegrationEvents;
using Evently.Common.Application.EventBus;
using Evently.Modules.Events.IntegrationEvents;
using Evently.Modules.Ticketing.Application.Abstractions.Payments;
using Evently.Modules.Ticketing.Domain.Orders;
using Evently.Modules.Ticketing.Domain.Payments;
using Evently.Modules.Ticketing.Infrastructure.Orders;
using Evently.Modules.Ticketing.Infrastructure.Payments;
using Evently.Modules.Ticketing.Infrastructure.Tickets;
using Evently.Modules.Ticketing.Domain.Tickets;

namespace Evently.Modules.Ticketing.Infrastructure;

public static class TicketingModule
{
    public static void ConfigureConsumers(IRegistrationConfigurator configure, string instanceId)
    {
        configure.AddConsumer<IntegrationEventConsumer<UserRegisteredIntegrationEvent>>()
            .Endpoint(c => c.InstanceId = instanceId);
        configure.AddConsumer<IntegrationEventConsumer<EventPublishedIntegrationEvent>>()
            .Endpoint(c => c.InstanceId = instanceId);
        /*configure.AddConsumer<TicketTypeCreatedIntegrationEventConsumer>()
            .Endpoint(c => c.InstanceId = instanceId);*/
    }

    public static IServiceCollection AddTicketingModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDomainEventHandlers();
        services.AddIntegrationEventHandlers();

        // presentation layer endpoints registration
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        // add module-specific settins
        services.AddInfrastructure(configuration);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        return services;
    }

    /// <summary>
    /// Добавить сервисы инфраструктуры
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<TicketingDbContext>((sp, options) =>
            options
                .UseSqlServer(
                    databaseConnectionString,
                    sqlOptions => sqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Ticketing))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetService<InsertOutboxMessagesInterceptor>()!)
        );

        services.AddSingleton<CartService>();

        // Регистрация репозиториев
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TicketingDbContext>());

        // Регистрация сервиса обработки платежей
        services.AddSingleton<IPaymentService, PaymentService>();

        services.Configure<OutboxOptions>(configuration.GetSection("Ticketing:Outbox"));
        services.ConfigureOptions<ConfigureProcessOutboxJob>();
        services.Configure<InboxOptions>(configuration.GetSection("Ticketing:Inbox"));
        services.ConfigureOptions<ConfigureProcessInboxJob>();
    }

    private static void AddIntegrationEventHandlers(this IServiceCollection services)
    {
        Type[] integrationEventHandlers = Presentation.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler)))
            .ToArray();

        foreach (Type integrationEventHandler in integrationEventHandlers)
        {
            services.TryAddScoped(integrationEventHandler);

            Type integrationEvent = integrationEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler =
                typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(integrationEvent);

            services.Decorate(integrationEventHandler, closedIdempotentHandler);
        }
    }

    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        Type[] domainEventHandlers = Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler)))
            .ToArray();

        foreach (Type handler in domainEventHandlers)
        {
            services.TryAddScoped(handler);

            Type domainEvent = handler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<>)
                .MakeGenericType(domainEvent);

            services.Decorate(handler, closedIdempotentHandler);
        }
    }
}
