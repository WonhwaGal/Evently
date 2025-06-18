using Evently.Common.Domain;

namespace Evently.Modules.Attendance.Domain.Tickets.Events;

/// <summary>
/// Доменное событие, сигнализирующее о том, что билет был создан
/// </summary>
/// <param name="ticketId">Идентификатор билета</param>
/// <param name="eventId">Идентификатор мероприятия, к которому относится билет</param>
public sealed class TicketCreatedDomainEvent(Guid ticketId, Guid eventId) : DomainEvent
{
    /// <summary>
    /// Идентификатор билета, который был создан
    /// </summary>
    public Guid TicketId { get; init; } = ticketId;

    /// <summary>
    /// Идентификатор мероприятия, к которому относится билет
    /// </summary>
    public Guid EventId { get; init; } = eventId;
}
