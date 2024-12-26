
namespace Evently.Modules.Ticketing.Application.Carts.GetCart;

public sealed class CartResponse
{
    public Guid CustomerId { get; set; }

    public List<CartItem> Items { get; init; } = [];
}
