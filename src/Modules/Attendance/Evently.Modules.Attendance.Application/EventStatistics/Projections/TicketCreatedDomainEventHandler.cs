using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Modules.Attendance.Domain.Tickets.Events;

namespace Evently.Modules.Attendance.Application.EventStatistics.Projections;

/// <summary>
/// Обработчик события, возникающего при создании билета
/// </summary>
/// <param name="dbConnectionFactory"></param>
internal sealed class TicketCreatedDomainEventHandler(IDbConnectionFactory dbConnectionFactory)
    : DomainEventHandler<TicketCreatedDomainEvent>
{
    /// <summary>
    /// Обработать событие создания билета
    /// </summary>
    /// <param name="domainEvent">Событие создания билета</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public override async Task Handle(
        TicketCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        // Открыть соединение с базой данных
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);
        // Подготовить SQL-запрос для обновления статистики мероприятия
        const string sql =
            """
            UPDATE attendance.event_statistics
            SET tickets_sold = (
                SELECT COUNT(*)
                FROM attendance.tickets t
                WHERE t.event_id = event_id)
            WHERE event_id = @EventId
            """;
        // Выполнить SQL-запрос с передачей параметров из события
        await connection.ExecuteAsync(sql, domainEvent);
    }
}

