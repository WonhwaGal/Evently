using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Abstractions;
using Evently.Modules.Events.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.Infrastructure.Database;
public sealed class EventsDbContext(
    DbContextOptions<EventsDbContext> options): DbContext(options), IUnitOfWork
{
    /// <summary>
    /// Мероприятие (концерт, фестиваль, выставка и т.д.)
    /// </summary>
    internal DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Events);
    }

    //public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
    //    PublishDomainEventsAsync();
    //    return await base.SaveChangesAsync(cancellationToken);
    //}

    //private void PublishDomainEventsAsync()
    //{
    //    var domainEvents = ChangeTracker
    //        .Entries<Entity>()
    //        .Select(entry => entry.Entity)
    //        .SelectMany(entity =>
    //        {
    //            IReadOnlyCollection<IDomainEvent> domainEvents = entity.DomainEvents;
    //            entity.ClearDomainEvents();
    //            return domainEvents;
    //        })
    //        .ToList();

    //    domainEvents.ForEach(async domainEvent => await publisher.Publish(domainEvent));
    //}
}
