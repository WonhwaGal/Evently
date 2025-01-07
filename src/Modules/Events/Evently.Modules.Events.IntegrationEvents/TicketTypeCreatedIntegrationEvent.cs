using Evently.Common.Application.EventBus;

namespace Evently.Modules.Events.IntegrationEvents;
public sealed class TicketTypeCreatedIntegrationEvent : IntegrationEvent
{
    public TicketTypeCreatedIntegrationEvent(
         Guid id,
         DateTime occurredOnUtc,
         Guid ticketTypeId,
         Guid eventId,
         string name,
         decimal price,
         string currency,
         decimal quantity) : base(id, occurredOnUtc)
    {
        IntegrationEventId = id;
        OccurredOnUtc = occurredOnUtc;
        TicketTypeId = ticketTypeId;
        EventId = eventId;
        Name = name;
        Price = price;
        Currency = currency;
        Quantity = quantity;
    }

    public TicketTypeCreatedIntegrationEvent() { }

    public Guid TicketTypeId { get; init; }
    public Guid EventId { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public decimal Quantity { get; init; }
    public string Currency { get; init; }
}
