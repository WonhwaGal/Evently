using Evently.Modules.Attendance.Domain.Events;

namespace Evently.Modules.Attendance.Application.EventStatistics.GetEventStatistics;

/// <summary>
/// Класс для преобразования статистики мероприятия в DTO-объект
/// </summary>
public static class EventStatisticsMappings
{
    /// <summary>
    /// Преобразовать объект статистики мероприятия в DTO-ответ
    /// </summary>
    /// <param name="eventStatistics">Статистика мероприятия</param>
    /// <returns></returns>
    public static EventStatisticsResponse ToDto(this EventStatisticsV2 eventStatistics)
    {
        return new EventStatisticsResponse(
            eventStatistics.EventId,
            eventStatistics.Title,
            eventStatistics.Description,
            eventStatistics.Location,
            eventStatistics.StartsAtUtc,
            eventStatistics.EndsAtUtc,
            eventStatistics.TicketsSold,
            eventStatistics.AttendeesCheckedIn)
        {
            TicketsSold = eventStatistics.TicketsSold,
            AttendeesCheckedIn = eventStatistics.AttendeesCheckedIn
        };
    }
}
