using Evently.Common.Application.Messaging;

namespace Evently.Modules.Ticketing.Application.TicketTypes.CreateTicketType;
public sealed record CreateTicketTypeCommand : ICommand
{
    public Guid TicketTypeId { get; init; }
    public Guid EventId { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public string Currency { get; init; }
    public decimal Quantity { get; init; }

    private CreateTicketTypeCommand() { }

    public CreateTicketTypeCommand(Guid ticketTypeId,
        Guid eventId,
        string name,
        decimal price,
        string currency,
        decimal quantity)
    {
        TicketTypeId = ticketTypeId;
        EventId = eventId;
        Name = name;
        Price = price;
        Currency = currency;
        Quantity = quantity;
    }
}
