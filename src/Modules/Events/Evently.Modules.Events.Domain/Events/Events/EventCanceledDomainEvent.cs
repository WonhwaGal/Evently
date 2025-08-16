using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Events.Events;

public sealed class EventCanceledDomainEvent(Guid eventId) : DomainEvent
{
    public Guid EventId { get; init; } = eventId;
}
