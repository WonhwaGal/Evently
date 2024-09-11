using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Infrastructure.Database;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Events.Infrastructure;
public static class EventsModule
{
    public static IServiceCollection AddEventsModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        string dbConnectionString = configuration.GetConnectionString("Database")!;
        services.AddDbContext<EventsDbContext>(options =>
            options.UseSqlServer(dbConnectionString, sqlOptions => sqlOptions
            .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
            .UseSnakeCaseNamingConvention());
        return services;
    }
}
