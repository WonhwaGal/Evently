using Evently.Common.Domain;

namespace Evently.Modules.Attendance.Domain.Tickets.Events;

/// <summary>
/// Доменное событие, сигнализирующее о том, что билет был использован
/// </summary>
/// <param name="ticketId">Идентификатор билета</param>
public sealed class TicketUsedDomainEvent(Guid ticketId) : DomainEvent
{
    /// <summary>
    /// Идентификатор билета, который был использован
    /// </summary>
    public Guid TicketId { get; init; } = ticketId;
}

