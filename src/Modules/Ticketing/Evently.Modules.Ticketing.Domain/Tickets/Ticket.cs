using Evently.Common.Domain;
using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.Domain.Orders;
using Evently.Modules.Ticketing.Domain.TicketTypes;

namespace Evently.Modules.Ticketing.Domain.Tickets;

/// <summary>
/// Сущность "Билет"
/// </summary>
public sealed class Ticket : Entity
{
    private Ticket()
    {
    }

    /// <summary>
    /// Идентификатор билета
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Идентификатор заказчика
    /// </summary>
    public Guid CustomerId { get; private set; }

    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public Guid OrderId { get; private set; }

    /// <summary>
    /// Идентификатор мероприятия
    /// </summary>
    public Guid EventId { get; private set; }

    /// <summary>
    /// Идентификатор типа билета
    /// </summary>
    public Guid TicketTypeId { get; private set; }

    /// <summary>
    /// Код билета
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAtUtc { get; private set; }

    /// <summary>
    /// Признак архивирования
    /// </summary>
    public bool Archived { get; private set; }

    /// <summary>
    /// Фабричный метод для создания нового объекта билета
    /// </summary>
    /// <param name="order"> Заказ </param>
    /// <param name="ticketType"> Тип билета </param>
    /// <returns></returns>
    public static Ticket Create(Order order, TicketType ticketType)
    {
        // Создать новый билет
        var ticket = new Ticket
        {
            Id = Guid.NewGuid(),
            CustomerId = order.CustomerId,
            OrderId = order.Id,
            EventId = ticketType.EventId,
            TicketTypeId = ticketType.Id,
            Code = $"tc_{Guid.NewGuid()}", // Сгенерировать уникальный код билета
            CreatedAtUtc = DateTime.UtcNow
        };

        ticket.Raise(new TicketCreatedDomainEvent(ticket.Id));

        return ticket;
    }

    /// <summary>
    /// Архивировать билет
    /// </summary>
    public void Archive()
    {
        // Если билет уже архивирован, то ничего не делаем
        if (Archived)
        {
            return;
        }
        // Архивируем билет
        Archived = true;
        // Возбудить событие: билет архивирован
        Raise(new TicketArchivedDomainEvent(Id, Code));
    }
}
