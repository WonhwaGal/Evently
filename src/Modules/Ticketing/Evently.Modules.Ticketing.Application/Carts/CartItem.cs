namespace Evently.Modules.Ticketing.Application.Carts;

/// <summary>
/// Позиция товара в корзине
/// </summary>
public sealed class CartItem
{
    /// <summary>
    /// Идентификатор типа билета
    /// </summary>
    public Guid TicketTypeId { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// Стоимость
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Валюта
    /// </summary>
    public string Currency { get; set; }
}
