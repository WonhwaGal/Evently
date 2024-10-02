using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Domain.Abstractions;

namespace Evently.Modules.Events.Domain.Events.Events;
public sealed class EventRescheduledDomainEvent(Guid eventId, DateTime statrsAtUtc, DateTime? endsAtUtc) 
    : DomainEvent
{
    public Guid EventId { get; } = eventId;

    public DateTime StartAtUtc { get; } = statrsAtUtc;

    public DateTime? EndsAtUtc { get; } = endsAtUtc;
}
