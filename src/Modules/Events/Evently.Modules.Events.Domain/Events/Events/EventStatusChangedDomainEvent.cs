using Evently.Modules.Events.Domain.Abstractions;

namespace Evently.Modules.Events.Domain.Events.Events;

public sealed class EventStatusChangedDomainEvent(Guid eventId, EventStatus newStatus): DomainEvent
{
    public Guid EventId { get; } = eventId;

    public EventStatus EventStatus { get; } = newStatus;
}
