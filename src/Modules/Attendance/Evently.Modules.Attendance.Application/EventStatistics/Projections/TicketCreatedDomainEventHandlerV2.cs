using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Modules.Attendance.Domain.Events;
using Evently.Modules.Attendance.Domain.Tickets.Events;

namespace Evently.Modules.Attendance.Application.EventStatistics.Projections;

internal sealed class TicketCreatedDomainEventHandlerV2(
    IDbConnectionFactory dbConnectionFactory,
    IEventStatisticsRepositoryV2 eventStatisticsRepository)
    : DomainEventHandler<TicketCreatedDomainEvent>
{
    public override async Task Handle(
        TicketCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        const string sql =
            """
            SELECT COUNT(*)
            FROM attendance.tickets t
            WHERE t.event_id = @EventId
            """;

        int ticketCount = await connection.ExecuteScalarAsync<int>(sql, domainEvent);

        EventStatisticsV2 eventStatistics =
            await eventStatisticsRepository.GetAsync(domainEvent.EventId, cancellationToken);

        eventStatistics.TicketsSold = ticketCount;

        await eventStatisticsRepository.ReplaceAsync(eventStatistics, cancellationToken);
    }
}

