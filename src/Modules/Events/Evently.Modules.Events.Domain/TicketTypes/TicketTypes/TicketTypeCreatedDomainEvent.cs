using Evently.Modules.Events.Domain.Abstractions;

namespace Evently.Modules.Events.Domain.TicketTypes.TicketTypes;
public sealed class TicketTypeCreatedDomainEvent(Guid ticketId): DomainEvent
{
    public Guid TicketTypeId { get; } = ticketId;
}
