using MongoDB.Bson.Serialization.Attributes;

namespace Evently.Modules.Attendance.Domain.Events;

/// <summary>
/// Статистика мероприятия
/// </summary>
public sealed class EventStatisticsV2
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
    public static EventStatisticsV2 Create(
        Guid id,
        string title,
        string description,
        string location,
        DateTime startsAtUtc,
        DateTime? endsAtUtc)
    {
        var @event = new EventStatisticsV2
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
    [BsonId] // Используется как уникальный идентификатор для мероприятия и для сериализации в MongoDB
    public Guid EventId { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Место проведения
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// Дата и время начала
    /// </summary>
    public DateTime StartsAtUtc { get; set; }

    /// <summary>
    /// Дата и время окончания
    /// </summary>
    public DateTime? EndsAtUtc { get; set; }

    /// <summary>
    /// Кол-во проданных билетов
    /// </summary>
    public int TicketsSold { get; set; }

    /// <summary>
    /// Кол-во посетителей, зарегистрированных на мероприятие
    /// </summary>
    public int AttendeesCheckedIn { get; set; }

    /// <summary>
    /// Билеты с повторяющейся регистрацией
    /// </summary>
    public ICollection<TicketModel> DuplicateCheckInTickets { get; set; } = [];

    /// <summary>
    /// Билеты с недействительной попыткой регистрации
    /// </summary>
    public ICollection<TicketModel> InvalidCheckInTickets { get; set; } = [];
}

