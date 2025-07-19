using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.TicketTypes.TicketTypes;
public sealed class TicketTypeCreatedDomainEvent: DomainEvent
{
    public Guid TicketTypeId { get; init; }

    public TicketTypeCreatedDomainEvent() { }

    public TicketTypeCreatedDomainEvent(Guid ticketTypeId)
    {
        TicketTypeId = ticketTypeId;
    }
}
