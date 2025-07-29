using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Attendance.Domain.Events;

namespace Evently.Modules.Attendance.Application.EventStatistics.GetEventStatistics;

/// <summary>
/// Обработчик запроса для получения статистики события
/// </summary>
/// <param name="eventStatisticsRepository"></param>
internal sealed class GetEventStatisticsQueryHandlerV2(
    IEventStatisticsRepositoryV2 eventStatisticsRepository)
    : IQueryHandler<GetEventStatisticsQueryV2, EventStatisticsResponse>
{
    /// <summary>
    /// Обработать запрос для получения статистики события
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public async Task<Result<EventStatisticsResponse>> Handle(
        GetEventStatisticsQueryV2 request,
        CancellationToken cancellationToken)
    {
        // Получить статистику мероприятия из репозитория
        EventStatisticsV2 eventStatistics =
            await eventStatisticsRepository.GetAsync(request.EventId, cancellationToken);

        // Вернуть статистику мероприятия в виде DTO-объекта
        return eventStatistics.ToDto();
    }
}

