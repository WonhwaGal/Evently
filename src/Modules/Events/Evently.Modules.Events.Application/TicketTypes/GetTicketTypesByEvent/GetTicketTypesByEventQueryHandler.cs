using System.Data.Common;
using Dapper;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Messaging;
using Evently.Modules.Events.Domain.Abstractions;

namespace Evently.Modules.Events.Application.TicketTypes.GetByEvent;

internal sealed class GetTicketTypesByEventQueryHandler(
    IDbConnectionFactory dbConnectionFactory
    ) : IQueryHandler<GetTicketTypesByEventQuery, IReadOnlyList<TicketTypeResponse>>
{
    public async Task<Result<IReadOnlyList<TicketTypeResponse>>> Handle(GetTicketTypesByEventQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        Guid eventId = request.EventId;

        const string sql =
            $"""
            SELECT
                t.Id AS {nameof(TicketTypeResponse.Id)},
                t.Event_id AS {nameof(TicketTypeResponse.EventId)},
                t.Name AS {nameof(TicketTypeResponse.Name)},
                t.Price AS {nameof(TicketTypeResponse.Price)},
                t.Currency AS {nameof(TicketTypeResponse.Currency)},
                t.Quantity AS {nameof(TicketTypeResponse.Quantity)}
            FROM
                events.ticket_types AS t
            WHERE
                t.Event_id = @eventId
            """;

        IEnumerable<TicketTypeResponse> @events = await dbConnection
            .QueryAsync<TicketTypeResponse>(
            sql,
            new
            {
                eventId
            });

        return @events.ToList();
    }
}
