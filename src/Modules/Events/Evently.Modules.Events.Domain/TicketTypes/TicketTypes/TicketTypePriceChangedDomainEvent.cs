using Evently.Modules.Events.Domain.Abstractions;

namespace Evently.Modules.Events.Domain.TicketTypes.TicketTypes;

public sealed class TicketTypePriceChangedDomainEvent(Guid id, decimal price) : DomainEvent
{
    public Guid TicketTypeID { get; } = id;

    public decimal TicketPrice { get; } = price;
}
