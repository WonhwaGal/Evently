namespace Evently.Modules.Ticketing.Application.Carts;
public sealed class Cart
{
    /// <summary>
    /// Идентификатор покупателя
    /// </summary>
    public Guid CustomerId { get; init; }

    /// <summary>
    /// Позиции товаров в корзине
    /// </summary>
    public List<CartItem> Items { get; init; } = [];

    /// <summary>
    /// Статический метод создания корзины по умолчанию
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    internal static Cart CreateDefault(Guid customerId) => new() { CustomerId = customerId };
}
