namespace Evently.Modules.Attendance.Domain.Events;

/// <summary>
/// Статистика мероприятия
/// </summary>
public sealed class EventStatistics
{
    /// <summary>
    /// Статический фабричный метод для создания экземпляра класса
    /// </summary>
    /// <param name="id">Идентификатор мероприятия</param>
    /// <param name="title">Наименование</param>
    /// <param name="description">Описание</param>
    /// <param name="location">Место проведения</param>
    /// <param name="startsAtUtc">Дата и время начала</param>
    /// <param name="endsAtUtc">Дата и время окончания</param>
    /// <returns>Экземпляр класса</returns>
    public static EventStatistics Create(
        Guid id,
        string title,
        string description,
        string location,
        DateTime startsAtUtc,
        DateTime? endsAtUtc)
    {
        var @event = new EventStatistics
        {
            EventId = id,
            Title = title,
            Description = description,
            Location = location,
            StartsAtUtc = startsAtUtc,
            EndsAtUtc = endsAtUtc,
            DuplicateCheckInTickets = [],
            InvalidCheckInTickets = []
        };
        return @event;
    }

    /// <summary>
    /// Идентификатор мероприятия
    /// </summary>
    public Guid EventId { get; private set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Место проведения
    /// </summary>
    public string Location { get; private set; }

    /// <summary>
    /// Дата и время начала
    /// </summary>
    public DateTime StartsAtUtc { get; private set; }

    /// <summary>
    /// Дата и время окончания
    /// </summary>
    public DateTime? EndsAtUtc { get; private set; }

    /// <summary>
    /// Кол-во проданных билетов
    /// </summary>
    public int TicketsSold { get; private set; }

    /// <summary>
    /// Кол-во посетителей, зарегистрированных на мероприятие
    /// </summary>
    public int AttendeesCheckedIn { get; private set; }

    /// <summary>
    /// Билеты с повторяющейся регистрацией
    /// </summary>
    public ICollection<string> DuplicateCheckInTickets { get; private set; }

    /// <summary>
    /// Билеты с недействительной попыткой регистрации
    /// </summary>
    public ICollection<string> InvalidCheckInTickets { get; private set; }
}

