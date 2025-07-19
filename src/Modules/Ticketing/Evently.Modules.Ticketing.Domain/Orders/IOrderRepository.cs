namespace Evently.Modules.Ticketing.Domain.Orders;

/// <summary>
/// Интерфейс описывает контракт репозитория заказов
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор заказа </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns></returns>
    Task<Order?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить заказ
    /// </summary>
    /// <param name="order"> Заказ </param>
    void Insert(Order order);
}
