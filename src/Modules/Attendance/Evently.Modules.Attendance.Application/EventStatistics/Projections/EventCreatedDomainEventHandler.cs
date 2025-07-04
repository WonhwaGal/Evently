using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Modules.Attendance.Domain.Events.Events;

namespace Evently.Modules.Attendance.Application.EventStatistics.Projections;

/// <summary>
/// Обработчик события создания мероприятия (EventCreatedDomainEvent)
/// </summary>
/// <param name="dbConnectionFactory"></param>
internal sealed class EventCreatedDomainEventHandler(IDbConnectionFactory dbConnectionFactory)
    : DomainEventHandler<EventCreatedDomainEvent>
{
    /// <summary>
    /// Обработать событие создания мероприятия
    /// </summary>
    /// <param name="domainEvent"></param>
    /// <param name="cancellationToken"></param>
    public override async Task Handle(
        EventCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        // Открыть соединение с базой данных
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);
        // Подготовить SQL-запрос для вставки новой записи в таблицу статистики мероприятий
        const string sql =
            """
            INSERT INTO attendance.event_statistics(
                event_id,
                title,
                description,
                location,
                starts_at_utc,
                ends_at_utc,
                tickets_sold,
                attendees_checked_in,
                duplicate_check_in_tickets,
                invalid_check_in_tickets)
            VALUES (
                @EventId,
                @Title,
                @Description,
                @Location,
                @StartsAtUtc,
                @EndsAtUtc,
                @TicketsSold,
                @AttendeesCheckedIn,
                @DuplicateCheckInTickets,
                @InvalidCheckInTickets)
            """;

        // Выполнить SQL-запрос с передачей параметров из события
        await connection.ExecuteAsync(
            sql,
            new
            {
                domainEvent.EventId,
                domainEvent.Title,
                domainEvent.Description,
                domainEvent.Location,
                domainEvent.StartsAtUtc,
                domainEvent.EndsAtUtc,
                TicketsSold = 0,
                AttendeesCheckedIn = 0,
                DuplicateCheckInTickets = "[]",
                InvalidCheckInTickets = "[]"
            });
    }
}

