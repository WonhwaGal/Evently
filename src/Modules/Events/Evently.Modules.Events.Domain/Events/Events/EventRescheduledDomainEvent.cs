using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Events.Events;
public sealed class EventRescheduledDomainEvent(Guid eventId, DateTime statrsAtUtc, DateTime? endsAtUtc) 
    : DomainEvent
{
    public Guid EventId { get; } = eventId;

    public DateTime StartAtUtc { get; } = statrsAtUtc;

    public DateTime? EndsAtUtc { get; } = endsAtUtc;
}
