using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Tickets.Events;

/// <summary>
/// Событие создания билета
/// </summary>
/// <param name="ticketId"> Идентификатор билета </param>
public sealed class TicketCreatedDomainEvent(Guid ticketId) : DomainEvent
{
    
    /// <summary>
    /// Идентификатор билета
    /// </summary>
    public Guid TicketId { get; init; } = ticketId;
    
}
