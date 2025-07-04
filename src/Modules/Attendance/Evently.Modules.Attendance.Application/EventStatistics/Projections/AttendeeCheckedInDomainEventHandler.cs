using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Modules.Attendance.Domain.Attendees.Events;

namespace Evently.Modules.Attendance.Application.EventStatistics.Projections;

/// <summary>
/// Обработчик события, возникающего при регистрации участника
/// на мероприятие (AttendeeCheckedInDomainEvent)
/// </summary>
/// <param name="dbConnectionFactory"></param>
internal sealed class AttendeeCheckedInDomainEventHandler(IDbConnectionFactory dbConnectionFactory)
    : DomainEventHandler<AttendeeCheckedInDomainEvent>
{
    /// <summary>
    /// Обработать событие регистрации участника на мероприятие
    /// </summary>
    /// <param name="domainEvent">Доменное событие регистрации участника на мероприятие</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public override async Task Handle(
        AttendeeCheckedInDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        // Открыть соединение с базой данных
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);
        // Подготовить SQL-запрос для обновления статистики мероприятия
        const string sql =
            """
            UPDATE attendance.event_statistics
            SET attendees_checked_in = (
                SELECT COUNT(*)
                FROM attendance.tickets t
                WHERE
                    t.event_id = event_id AND
                    t.used_at_utc IS NOT NULL)
            WHERE event_id = @EventId
            """;
        // Выполнить SQL-запрос с передачей параметров
        await connection.ExecuteAsync(sql, domainEvent);
    }
}
