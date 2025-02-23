using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Infrastructure.Interceptors;
using Evently.Common.Infrastructure.Outbox;
using Evently.Modules.Users.Application.Abstractions.Data;
using Evently.Modules.Users.Application.Abstractions.Identity;
using Evently.Modules.Users.Domain.Users;
using Evently.Modules.Users.Infrastructure.Database;
using Evently.Modules.Users.Infrastructure.Identity;
using Evently.Modules.Users.Infrastructure.Outbox;
using Evently.Modules.Users.Infrastructure.PublicApi;
using Evently.Modules.Users.Infrastructure.Users;
using Evently.Modules.Users.Presentation.Users;
using Evently.Modules.Users.PublicApi;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        services.Configure<KeyCloakOptions>(configuration.GetSection("Users:KeyCloak"));
        services.AddTransient<KeyCloakAuthDelegatingHandler>();

        services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpClient) =>
        {
            KeyCloakOptions keycloakOptions = serviceProvider
                .GetRequiredService<IOptions<KeyCloakOptions>>().Value;

            httpClient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
        });

        services.AddHttpClient<KeyCloakClient>((serviceProvider, httpClient) =>
        {
            KeyCloakOptions keyCloakOptions = serviceProvider
                .GetRequiredService<IOptions<KeyCloakOptions>>().Value;
            httpClient.BaseAddress = new Uri(keyCloakOptions.AdminUrl);
        }).AddHttpMessageHandler<KeyCloakAuthDelegatingHandler>();

        services.AddTransient<IIdentityProviderService, IdentityProviderService>();


        string connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<UserDbContext>((sp, options) =>
            options.UseSqlServer(connectionString, sqlOptions =>
            sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
            .UseSnakeCaseNamingConvention()
            .AddInterceptors(sp.GetService<InsertOutboxMessagesInterceptor>()!));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UserDbContext>());

        // repositories
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUsersApi, UsersApi>();

        services.Configure<OutboxOptions>(configuration.GetSection("Users:Outbox"));
        services.ConfigureOptions<ConfigureProcessOutboxJob>();
    }
}
