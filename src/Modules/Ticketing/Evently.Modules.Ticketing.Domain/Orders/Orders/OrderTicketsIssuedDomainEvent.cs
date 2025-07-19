using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Orders;

/// <summary>
/// Событие: заказ выдан
/// </summary>
/// <param name="orderId"> Идентификатор заказа </param>
public sealed class OrderTicketsIssuedDomainEvent(Guid orderId) : DomainEvent
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public Guid OrderId { get; init; } = orderId;
}
