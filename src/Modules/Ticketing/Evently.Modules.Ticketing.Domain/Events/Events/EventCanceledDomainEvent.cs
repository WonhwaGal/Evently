using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Events;

/// <summary>
/// Событие отмены мероприятия
/// </summary>
/// <param name="eventId"> Идентификатор мероприятия </param>
public sealed class EventCanceledDomainEvent(Guid eventId) : DomainEvent
{
    public Guid EventId { get; } = eventId;
}
