using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Application.Data;
using Evently.Common.Infrastructure.Interceptors;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Categories;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Domain.TicketTypes;
using Evently.Modules.Events.Infrastructure.Categories;
using Evently.Modules.Events.Infrastructure.Database;
using Evently.Modules.Events.Infrastructure.Events;
using Evently.Modules.Events.Infrastructure.TicketTypes;
using Evently.Modules.Events.Presentation.Categories;
using Evently.Modules.Events.Presentation.Events;
using Evently.Modules.Events.Presentation.TicketTypes;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Evently.Modules.Events.Infrastructure;
public static class EventsModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        EventEndpoints.MapEndpoints(app);
        CategoriesEndpoints.MapEndpoints(app);
        TicketTypesEndpoints.MapEndpoints(app);
    }

    public static IServiceCollection AddEventsModule(this IServiceCollection services,
        IConfiguration configuration)
    {

        // Добавить сервисы инфраструктуры
        services.AddInfrastructure(configuration);

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

        services.AddDbContext<EventsDbContext>((sp, options) =>
            options.UseSqlServer(databaseConnectionString,
                    sqlOptions => sqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetService<PublishDomainEventsInterceptor>()!)
        );

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());

        // Регистрация репозиториев
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
    }
}
