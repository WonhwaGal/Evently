using Bogus;
using Evently.Modules.Events.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Events.IntegrationTests.Abstractions;

[Collection(nameof(IntegrationTestCollection))]
public abstract class BaseIntegrationTest : IDisposable
{
    private readonly IServiceScope _scope;
    protected static readonly Faker Faker = new();
    protected readonly IntegrationTestWebAppFactory Factory;
    protected readonly ISender Sender;
    protected readonly EventsDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Factory = factory;
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<EventsDbContext>();
    }

    protected async Task CleanDatabaseAsync()
    {
        await DbContext.Database.ExecuteSqlRawAsync(
            """
            DELETE FROM events.ticket_types;
            DELETE FROM events.events;
            DELETE FROM events.categories;
            """);
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
