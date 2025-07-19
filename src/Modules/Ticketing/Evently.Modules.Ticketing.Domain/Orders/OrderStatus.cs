namespace Evently.Modules.Ticketing.Domain.Orders;

/// <summary>
/// Статус заказа
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Ожидает оплаты
    /// </summary>
    Pending = 0,
    
    /// <summary>
    /// Оплачен
    /// </summary>
    Paid = 1,
    
    /// <summary>
    /// Возвращен
    /// </summary>
    Refunded = 2,
    
    /// <summary>
    /// Отменен
    /// </summary>
    Canceled = 3
}
