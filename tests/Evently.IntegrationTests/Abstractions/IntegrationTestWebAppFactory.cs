using Evently.Common.Application.Clock;
using Evently.Modules.Users.Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using Testcontainers.Keycloak;
using Testcontainers.MsSql;
using Testcontainers.Redis;

namespace Evently.IntegrationTests.Abstractions;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public readonly IDateTimeProvider DateTimeProviderMock = Substitute.For<IDateTimeProvider>();

    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-CU16-ubuntu-22.04")
        .Build();

    private readonly RedisContainer _redisContainer = new RedisBuilder()
        .WithImage("redis:latest")
        .Build();

    private readonly KeycloakContainer _keyCloakContainer = new KeycloakBuilder()
        .WithImage("quay.io/keycloak/keycloak:26.0.0")
        .WithResourceMapping(
            new FileInfo("evently-realm-export.json"),
             new FileInfo("/opt/keycloak/data/import/realm.json"))
        .WithCommand("--import-realm")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("ConnectionStrings:Database", _dbContainer.GetConnectionString());
        Environment.SetEnvironmentVariable("ConnectionStrings:Cache", _redisContainer.GetConnectionString());
        builder.ConfigureTestServices(services =>
        {
#pragma warning disable CA2263
            services.RemoveAll(typeof(IDateTimeProvider));
#pragma warning restore CA2263

            DateTimeProviderMock.UtcNow.Returns(_ => DateTime.UtcNow);
            services.AddSingleton(DateTimeProviderMock);
        });


        string keycloakAddress = _keyCloakContainer.GetBaseAddress();
        string keyCloakRealmUrl = $"{keycloakAddress}realms/evently";

        Environment.SetEnvironmentVariable(
            "Authentication:MetadataAddress",
            $"{keyCloakRealmUrl}/.well-known/openid-configuration");
        Environment.SetEnvironmentVariable(
            "Authentication:TokenValidationParameters:ValidIssuer",
            keyCloakRealmUrl);

        builder.ConfigureTestServices(services =>
        {
            services.Configure<KeyCloakOptions>(o =>
            {
                o.AdminUrl = $"{keycloakAddress}admin/realms/evently/";
                o.TokenUrl = $"{keycloakAddress}realms/evently/protocol/openid-connect/token";
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _redisContainer.StartAsync();
        await _keyCloakContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _redisContainer.StopAsync();
        await _keyCloakContainer.StopAsync();
    }
}
