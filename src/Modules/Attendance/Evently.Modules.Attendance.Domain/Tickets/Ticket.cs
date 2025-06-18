using Evently.Common.Domain;
using Evently.Modules.Attendance.Domain.Attendees;
using Evently.Modules.Attendance.Domain.Events;
using Evently.Modules.Attendance.Domain.Tickets.Events;

namespace Evently.Modules.Attendance.Domain.Tickets;

/// <summary>
///  Билет (Ticket)
/// </summary>
public sealed class Ticket : Entity
{
    /// <summary>
    /// Статический метод для создания нового билета
    /// </summary>
    /// <param name="ticketId">Идентификатор билета</param>
    /// <param name="attendee">Посетитель мероприятия (Attendee)</param>
    /// <param name="event">Событие (Event)</param>
    /// <param name="code">Уникальный код билета</param>
    /// <returns></returns>
    public static Ticket Create(Guid ticketId, Attendee attendee, Event @event, string code)
    {
        // Создать новый билет
        var ticket = new Ticket
        {
            Id = ticketId,
            AttendeeId = attendee.Id,
            EventId = @event.Id,
            Code = code
        };
        // Возбудить событие, сигнализирующее о том, что билет был создан
        ticket.Raise(new TicketCreatedDomainEvent(ticket.Id, ticket.EventId));
        // Вернуть созданный билет
        return ticket;
    }

    /// <summary>
    ///  Пометить билет как использованный
    /// </summary>
    internal void MarkAsUsed()
    {
        // Изменить дату использования билета
        UsedAtUtc = DateTime.UtcNow;
        // Возбудить событие, сигнализирующее о том,
        // что билет был использован
        Raise(new TicketUsedDomainEvent(Id));
    }
    
    /// <summary>
    /// Конструктор по умолчанию (для EF Core)
    /// </summary>
    private Ticket()
    {
    }

    /// <summary>
    /// Идентификатор билета
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Идентификатор участника мероприятия (Attendee)
    /// </summary>
    public Guid AttendeeId { get; private set; }

    /// <summary>
    /// Идентификатор мероприятия (Event)
    /// </summary>
    public Guid EventId { get; private set; }

    /// <summary>
    /// Код билета
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    /// Дата и время использования билета в формате UTC
    /// </summary>
    public DateTime? UsedAtUtc { get; private set; }
}

