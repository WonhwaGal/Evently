using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Common.Infrastructure.Interceptors;
public class PublishDomainEventsInterceptor (
    IServiceScopeFactory serviceScopeFactory): SaveChangesInterceptor
{
    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        PublishDomainEventsAsync(eventData.Context!);
        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private void PublishDomainEventsAsync(DbContext context)
    {
        var domainEvents = context.ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                IReadOnlyCollection<IDomainEvent> domainEvents = entity.DomainEvents;
                entity.ClearDomainEvents();
                return domainEvents;
            })
            .ToList();

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
        domainEvents.ForEach(async domainEvent => await publisher.Publish(domainEvent));
    }
}
