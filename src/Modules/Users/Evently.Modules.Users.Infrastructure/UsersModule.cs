using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Infrastructure;
using Evently.Common.Infrastructure.Interceptors;
using Evently.Modules.Users.Application.Abstractions.Data;
using Evently.Modules.Users.Domain.Users;
using Evently.Modules.Users.Infrastructure.Database;
using Evently.Modules.Users.Infrastructure.Users;
using Evently.Modules.Users.Presentation.Users;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Users.Infrastructure;
public static class UsersModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        UserEndpoints.MapEndpoints(app);
    }

    public static IServiceCollection AddUsersModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        AddInfrastructure(services, configuration);

        return services;
    }

    private static void AddInfrastructure(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<UserDbContext>((sp, options) =>
            options.UseSqlServer(connectionString, sqlOptions =>
            sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
            .UseSnakeCaseNamingConvention()
            .AddInterceptors(sp.GetService<PublishDomainEventsInterceptor>()!));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UserDbContext>());

        // repositories
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
