namespace Evently.Modules.Events.Application.TicketTypes.GetTicketTypeById;
public sealed class TicketTypeResponse
{
    /// <summary>
    /// Идентификатор типа билета
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор мероприятия
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Название типа билета
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Стоимость билета
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Валюта билетов
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Количество билетов
    /// </summary>
    public decimal Quantity { get; set; }
}
