using Evently.Common.Application.Messaging;

namespace Evently.Modules.Attendance.Application.EventStatistics.GetEventStatistics;

/// <summary>
/// Запрос для получения статистики события
/// </summary>
/// <param name="EventId">Идентификатор события катор события</param>
public sealed record GetEventStatisticsQuery(Guid EventId) : IQuery<EventStatisticsResponse>;

