namespace Evently.Modules.Attendance.Application.EventStatistics.GetEventStatistics;

/// <summary>
/// Ответ на запрос получения статистики мероприятия
/// </summary>
/// <param name="EventId">Идентификатор</param>
/// <param name="Title">Наименование</param>
/// <param name="Description">Описание</param>
/// <param name="Location">Место проведения</param>
/// <param name="StartsAtUtc">Время начала проведения мероприятия в UTC</param>
/// <param name="EndsAtUtc">Время окончания проведения мероприятия в UTC</param>
/// <param name="TicketsSold">Количество проданных билетов</param>
/// <param name="AttendeesCheckedIn">Количество посетителей, прошедших регистрацию</param>
public sealed record EventStatisticsResponse(
    Guid EventId,
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc,
    int TicketsSold,
    int AttendeesCheckedIn)
{
    /// <summary>
    /// Список билетов с дублирующейся регистрацией
    /// </summary>
    public string[] DuplicateCheckInTickets { get; set; }

    /// <summary>
    /// Список билетов с недействительной регистрацией
    /// </summary>
    public string[] InvalidCheckInTickets { get; set; }
}
