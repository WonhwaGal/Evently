using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Domain.TicketTypes.TicketTypes;

namespace Evently.Modules.Events.Domain.TicketTypes;
public sealed class TicketType : Entity
{
    /// <summary>
    ///  Конструктор по умолчанию (для ORM)
    /// </summary>
    private TicketType()
    {
    }

    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Идентификатор события
    /// </summary>
    public Guid EventId { get; private set; }

    /// <summary>
    /// Название типа билета
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Цена
    /// </summary>
    public decimal Price { get; private set; }

    /// <summary>
    /// Валюта
    /// </summary>
    public string Currency { get; private set; }

    /// <summary>
    /// Количество
    /// </summary>
    public decimal Quantity { get; private set; }

    /// <summary>
    /// Создать тип билета
    /// </summary>
    /// <param name="event"> Событие </param>
    /// <param name="name"> Название типа билета </param>
    /// <param name="price"> Цена </param>
    /// <param name="currency"> Валюта </param>
    /// <param name="quantity"> Количество </param>
    /// <returns></returns>
    public static TicketType Create(
        Event @event,
        string name,
        decimal price,
        string currency,
        decimal quantity)
    {
        var ticketType = new TicketType
        {
            Id = Guid.NewGuid(),
            EventId = @event.Id,
            Name = name,
            Price = price,
            Currency = currency,
            Quantity = quantity
        };

        ticketType.Raise(new TicketTypeCreatedDomainEvent(ticketType.Id));

        return ticketType;
    }

    /// <summary>
    /// Обновление цены на определенный тип билета
    /// </summary>
    /// <param name="price"></param>
    public void UpdatePrice(decimal price)
    {
        if (Price == price)
        {
            return;
        }

        Price = price;

        Raise(new TicketTypePriceChangedDomainEvent(Id, Price));
    }
}

