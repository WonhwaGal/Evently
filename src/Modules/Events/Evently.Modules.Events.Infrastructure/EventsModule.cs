using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Infrastructure.Data;
using Evently.Modules.Events.Infrastructure.Database;
using Evently.Modules.Events.Infrastructure.Events;
using Evently.Modules.Events.Presentation.Events;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Events.Infrastructure;
public static class EventsModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        EventEndpoints.MapEndpoints(app);
    }

    public static IServiceCollection AddEventsModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Application.AssemblyReference.Assembly);
        });

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

        services.AddSingleton<IDbConnectionFactory>(_ => new SqlConnectionFactory(databaseConnectionString));


        services.AddDbContext<EventsDbContext>(options =>
            options.UseSqlServer(databaseConnectionString,
                    sqlOptions => sqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
                .UseSnakeCaseNamingConvention()); // Использовать соглашение об именовании в стиле snake_case


        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());

        // Регистрация репозиториев
        services.AddScoped<IEventRepository, EventRepository>();
    }
}
