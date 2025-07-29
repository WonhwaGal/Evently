using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Modules.Attendance.Domain.Attendees.Events;
using Evently.Modules.Attendance.Domain.Events;

namespace Evently.Modules.Attendance.Application.EventStatistics.Projections;

/// <summary>
/// Обработчик события, возникающего при регистрации участника
/// на мероприятие (AttendeeCheckedInDomainEvent)
/// </summary>
/// <param name="dbConnectionFactory"></param>
/// <param name="eventStatisticsRepository"></param>
internal sealed class AttendeeCheckedInDomainEventHandlerV2(
    IDbConnectionFactory dbConnectionFactory,
    IEventStatisticsRepositoryV2 eventStatisticsRepository)
    : DomainEventHandler<AttendeeCheckedInDomainEvent>
{
    public override async Task Handle(
        AttendeeCheckedInDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        // Открыть соединение с базой данных
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);
        // Подготовить SQL-запрос для обновления статистики мероприятия
        const string sql =
            """
            SELECT COUNT(*)
            FROM attendance.tickets t
            WHERE
                t.event_id = @EventId AND
                t.used_at_utc IS NOT NULL
            """;

        int attendeeCount = await connection.ExecuteScalarAsync<int>(sql, domainEvent);

        EventStatisticsV2 eventStatistics =
            await eventStatisticsRepository.GetAsync(domainEvent.EventId, cancellationToken);

        eventStatistics.AttendeesCheckedIn = attendeeCount;

        await eventStatisticsRepository.ReplaceAsync(eventStatistics, cancellationToken);
    }
}
