using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Tickets;

/// <summary>
/// Событие архивации тикета
/// </summary>
/// <param name="ticketId"> Идентификатор билета </param>
/// <param name="code"> Код билета </param>
public sealed class TicketArchivedDomainEvent(Guid ticketId, string code) : DomainEvent
{
    
    /// <summary>
    /// Идентификатор билета
    /// </summary>
    public Guid TicketId { get; init; } = ticketId;

    /// <summary>
    /// Код билета
    /// </summary>
    public string Code { get; init; } = code;
    
}
