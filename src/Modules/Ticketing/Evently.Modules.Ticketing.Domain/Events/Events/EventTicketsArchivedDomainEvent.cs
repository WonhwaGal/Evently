using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Events;

/// <summary>
/// Событие: билеты мероприятия архивированы
/// </summary>
/// <param name="eventId"> Идентификатор мероприятия </param>
public sealed class EventTicketsArchivedDomainEvent(Guid eventId) : DomainEvent
{
    
    /// <summary>
    /// Идентификатор мероприятия
    /// </summary>
    public Guid EventId { get; init; } = eventId;
    
}
