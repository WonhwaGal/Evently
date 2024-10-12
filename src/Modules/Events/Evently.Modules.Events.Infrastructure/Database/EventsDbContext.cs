using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Categories;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Domain.TicketTypes;
using Evently.Modules.Events.Infrastructure.Events;
using Evently.Modules.Events.Infrastructure.TicketTypes;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.Infrastructure.Database;
public sealed class EventsDbContext(
    DbContextOptions<EventsDbContext> options): DbContext(options), IUnitOfWork
{
    internal DbSet<Event> Events { get; set; }

    internal DbSet<Category> Categories { get; set; }

    internal DbSet<TicketType> TicketTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Events);
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new TicketTypeConfiguration());
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
