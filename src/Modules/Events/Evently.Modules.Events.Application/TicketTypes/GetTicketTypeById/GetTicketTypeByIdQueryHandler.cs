using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;

namespace Evently.Modules.Events.Application.TicketTypes.GetTicketTypeById;
internal sealed class GetTicketTypeByIdQueryHandler(
    IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetTicketTypeByIdQuery, TicketTypeResponse?>
{
    public async Task<Result<TicketTypeResponse?>> Handle(GetTicketTypeByIdQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection dbConnection = 
            await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        Guid ticketTypeId = request.Id;

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
                t.Id = @ticketTypeId
            """;

        TicketTypeResponse? ticketType = await dbConnection
            .QuerySingleOrDefaultAsync<TicketTypeResponse>(
            sql,
            new
            {
                ticketTypeId
            });

        return ticketType;
    }
}
