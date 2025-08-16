using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Orders.Events;

public sealed class OrderCreatedDomainEvent(Guid orderId) : DomainEvent
{
    public Guid OrderId { get; init; } = orderId;
}
