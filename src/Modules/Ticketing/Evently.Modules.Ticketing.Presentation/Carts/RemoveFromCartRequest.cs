namespace Evently.Modules.Ticketing.Presentation.Carts;

internal sealed class RemoveFromCartRequest
{
    public Guid CustomerId { get; set; }

    public Guid TicketTypeId { get; set; }
}
