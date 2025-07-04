using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Attendance.Domain.Events;
using Newtonsoft.Json;

namespace Evently.Modules.Attendance.Application.EventStatistics.GetEventStatistics;

/// <summary>
/// Обработчик запроса для получения статистики события
/// </summary>
/// <param name="dbConnectionFactory"></param>
internal sealed class GetEventStatisticsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetEventStatisticsQuery, EventStatisticsResponse>
{
    /// <summary>
    /// Обработать запрос для получения статистики события
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public async Task<Result<EventStatisticsResponse>> Handle(
        GetEventStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        // Получить соединение с базой данных
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        // Подготовить SQL-запрос для получения статистики события
        const string sql =
            $"""
             SELECT
                 event_id AS {nameof(EventStatisticsResponse.EventId)},
                 title AS {nameof(EventStatisticsResponse.Title)},
                 description AS {nameof(EventStatisticsResponse.Description)},
                 location AS {nameof(EventStatisticsResponse.Location)},
                 starts_at_utc AS {nameof(EventStatisticsResponse.StartsAtUtc)},
                 ends_at_utc AS {nameof(EventStatisticsResponse.EndsAtUtc)},
                 tickets_sold AS {nameof(EventStatisticsResponse.TicketsSold)},
                 attendees_checked_in AS {nameof(EventStatisticsResponse.AttendeesCheckedIn)},
                 duplicate_check_in_tickets AS {nameof(EventStatisticsResponse.DuplicateCheckInTickets)},
                 invalid_check_in_tickets AS {nameof(EventStatisticsResponse.InvalidCheckInTickets)}
             FROM
                 attendance.event_statistics
             WHERE
                 event_id = @EventId
             """;

        // Выполнить запрос и получить статистику события
        EventStatisticsResponse? eventStatistics =
            (await connection
                .QueryAsync<EventStatisticsResponse, string, string, EventStatisticsResponse>(
                    sql,
                    map: (eventStatisticsResponse, duplicateCheckInTickets, invalidCheckInTickets) =>
                    {
                        string[] duplicateCheckInTicketsList = JsonConvert
                            .DeserializeObject<ICollection<string>>(duplicateCheckInTickets)!
                            .ToArray();

                        string[] invalidCheckInTicketsList = JsonConvert
                            .DeserializeObject<ICollection<string>>(invalidCheckInTickets)!
                            .ToArray();

                        eventStatisticsResponse.DuplicateCheckInTickets = duplicateCheckInTicketsList;
                        eventStatisticsResponse.InvalidCheckInTickets = invalidCheckInTicketsList;

                        return eventStatisticsResponse;
                    },
                    request,
                    splitOn: $"{nameof(EventStatisticsResponse.DuplicateCheckInTickets)}, {nameof(EventStatisticsResponse.InvalidCheckInTickets)}"))
            .FirstOrDefault();

        // Если статистика события не найдена, вернуть ошибку
        if (eventStatistics is null)
        {
            return Result.Failure<EventStatisticsResponse>(EventErrors.NotFound(request.EventId));
        }

        // Вернуть статистику события
        return eventStatistics;
    }
}

