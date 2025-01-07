using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Infrastructure.Interceptors;
using Evently.Modules.Ticketing.Application.Abstractions.Data;
using Evently.Modules.Ticketing.Application.Carts;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.Infrastructure.Customers;
using Evently.Modules.Ticketing.Presentation.Carts;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Evently.Modules.Ticketing.Infrastructure.Database;
using Evently.Modules.Ticketing.Infrastructure.PublicApi;
using Evently.Modules.Ticketing.PublicApi;
using Evently.Modules.Ticketing.Domain.TicketTypes;
using Evently.Modules.Ticketing.Infrastructure.TicketTypes;
using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.Infrastructure.Events;
using Evently.Common.Presentation.Endpoints;
using MassTransit;
using Evently.Modules.Ticketing.Presentation.Customers;
using Evently.Modules.Ticketing.Presentation.Events;
using Evently.Modules.Ticketing.Presentation.TicketTypes;

namespace Evently.Modules.Ticketing.Infrastructure;

public static class TicketingModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CartEndpoints.MapEndpoints(app);
    }

    public static void ConfigureConsumers(IRegistrationConfigurator configure)
    {
        configure.AddConsumer<UserRegisteredIntegrationEventConsumer>();
        configure.AddConsumer<EventCreatedIntegrationEventConsumer>();
        configure.AddConsumer<TicketTypeCreatedIntegrationEventConsumer>();
    }


    public static IServiceCollection AddTicketingModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        // presentation layer endpoints registration
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<TicketingDbContext>((sp, options) =>
            options
                .UseSqlServer(
                    databaseConnectionString,
                    sqlOptions => sqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Ticketing))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetService<PublishDomainEventsInterceptor>()!));

        services.AddSingleton<CartService>();

        // Регистрация репозиториев
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        services.AddScoped<IEventRepository, EventRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TicketingDbContext>());

        // Регистрация сервиса предоставляющего публичное API
        services.AddScoped<ITicketingApi, TicketingApi>();

        return services;
    }

}
