using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.TicketTypes.Events;

public sealed class TicketTypePriceChangedDomainEvent(Guid id, decimal price) : DomainEvent
{
    public Guid TicketTypeID { get; } = id;

    public decimal TicketPrice { get; } = price;
}
