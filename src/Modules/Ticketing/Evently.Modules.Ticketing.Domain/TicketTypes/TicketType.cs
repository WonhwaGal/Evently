using Evently.Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evently.Modules.Ticketing.Domain.TicketTypes;

public sealed class TicketType : Entity
{
    /// <summary>
    /// Приватный конструктор (для ORM)
    /// </summary>
    private TicketType()
    {
    }

    /// <summary>
    /// Идентификатор типа билета
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Идентификатор мероприятия
    /// </summary>
    public Guid EventId { get; private set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Стоимость
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; private set; }

    /// <summary>
    /// Валюта
    /// </summary>
    public string Currency { get; private set; }

    /// <summary>
    /// Количество
    /// </summary>
    [Column(TypeName = "decimal(6,0)")]
    public decimal Quantity { get; private set; }

    /// <summary>
    /// Доступное количество
    /// </summary>
    public decimal AvailableQuantity { get; private set; }

    /// <summary>
    /// Фабричный метод для создания нового объекта типа билета
    /// </summary>
    /// <param name="id"> Идентификатор типа билета </param>
    /// <param name="eventId"> Идентификатор мероприятия </param>
    /// <param name="name"> Наименование </param>
    /// <param name="price"> Цена </param>
    /// <param name="currency"> Валюта </param>
    /// <param name="quantity"> Количество </param>
    /// <returns></returns>
    public static TicketType Create(
        Guid id,
        Guid eventId,
        string name,
        decimal price,
        string currency,
        decimal quantity)
    {
        var ticketType = new TicketType
        {
            Id = id,
            EventId = eventId,
            Name = name,
            Price = price,
            Currency = currency,
            Quantity = quantity,
            AvailableQuantity = quantity
        };

        return ticketType;
    }

    /// <summary>
    /// Обновить стоимость типа билета
    /// </summary>
    /// <param name="price"> Новая стоимость </param>
    public void UpdatePrice(decimal price)
    {
        Price = price; // Обновить стоимость
    }

    /// <summary>
    /// Обновить количество доступных билетов
    /// </summary>
    /// <param name="quantity"> Новое количество </param>
    /// <returns></returns>
    public Result UpdateQuantity(decimal quantity)
    {
        // Проверить, что новое количество больше или равно 0
        if (AvailableQuantity < quantity)
        {
            // Вернуть ошибку: недостаточное количество
            return Result.Failure(TicketTypeErrors.NotEnoughQuantity(AvailableQuantity));
        }
        // Обновить количество доступных билетов
        AvailableQuantity -= quantity;
        // Если количество доступных билетов равно 0
        if (AvailableQuantity == 0)
        {
            // Возбудить событие: тип билета распродан
            //Raise(new TicketTypeSoldOutDomainEvent(Id));
        }
        // Вернуть успешный результат выполнения операции
        return Result.Success();
    }
}
