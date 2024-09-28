using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.Infrastructure.Events;
internal sealed class EventRepository(EventsDbContext context) : IEventRepository
{
    public async Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Events.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Insert(Event @event)
    {
        context.Events.Add(@event);
    }

    public void CancelEvent(Guid id)
    {
        Event? @event = context.Events.SingleOrDefault(e => e.Id == id);

        if (@event is null)
        {
            throw new NotImplementedException();
        }

        if (@event.StartsAtUtc <= DateTime.Now)
        {
            throw new NotImplementedException();
        }

        @event.Status = EventStatus.Cancelled;
    }

    public void PublishEvent(Guid id)
    {
        Event? @event = context.Events.SingleOrDefault(e => e.Id == id);

        if (@event is null)
        {
            throw new NotImplementedException();
        }

        if (@event.StartsAtUtc <= DateTime.Now)
        {
            throw new NotImplementedException();
        }

        @event.Status = EventStatus.Published;
    }

    public void Reschedule(Guid eventId, DateTime? startsAtUtc, DateTime? endsAtUtc)
    {
        Event? @event = context.Events.SingleOrDefault(e => e.Id == eventId);

        if (@event is null)
        {
            throw new NotImplementedException();
        }

        if (startsAtUtc is not null)
        {
            @event.StartsAtUtc = (DateTime)startsAtUtc;
        }

        if(endsAtUtc is not null)
        {
            @event.EndsAtUtc = endsAtUtc;
        }
    }
}
