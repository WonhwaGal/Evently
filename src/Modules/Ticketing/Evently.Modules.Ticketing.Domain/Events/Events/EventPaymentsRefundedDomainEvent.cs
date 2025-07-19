using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Events;

/// <summary>
/// Событие возврата платежей за мероприятие
/// </summary>
/// <param name="eventId"> Идентификатор мероприятия </param>
public sealed class EventPaymentsRefundedDomainEvent(Guid eventId) : DomainEvent
{
    public Guid EventId { get; init; } = eventId;
}
