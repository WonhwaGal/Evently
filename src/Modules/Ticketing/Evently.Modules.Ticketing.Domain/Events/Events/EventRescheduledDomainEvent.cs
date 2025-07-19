using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Events;

/// <summary>
/// Событие переноса мероприятия
/// </summary>
/// <param name="eventId"> Идентификатор мероприятия </param>
/// <param name="startsAtUtc"> Дата начала мероприятия </param>
/// <param name="endsAtUtc"> Дата окончания мероприятия </param>
public sealed class EventRescheduledDomainEvent(Guid eventId, DateTime startsAtUtc, DateTime? endsAtUtc)
    : DomainEvent
{
    
    /// <summary>
    /// Идентификатор мероприятия
    /// </summary>
    public Guid EventId { get; } = eventId;

    /// <summary>
    /// Дата начала мероприятия
    /// </summary>
    public DateTime StartsAtUtc { get; } = startsAtUtc;

    /// <summary>
    /// Дата окончания мероприятия
    /// </summary>
    public DateTime? EndsAtUtc { get; } = endsAtUtc;
    
}
