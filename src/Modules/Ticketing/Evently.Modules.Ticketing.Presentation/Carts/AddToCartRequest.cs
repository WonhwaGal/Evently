
namespace Evently.Modules.Ticketing.Presentation.Carts;

internal sealed class AddToCartRequest
{
    public Guid CustomerId { get; init; }

    public Guid TicketTypeId { get; init; }

    public decimal Quantity { get; init; }
}
