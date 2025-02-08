using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypeById;

namespace Evently.Modules.Events.Application.TicketTypes.GetTicketTypes;
internal sealed class GetTicketTypesQueryHandler(
    IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetTicketTypesQuery, IReadOnlyList<TicketTypeResponse>>
{
    public async Task<Result<IReadOnlyList<TicketTypeResponse>>> Handle(GetTicketTypesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

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
            """;

        IEnumerable<TicketTypeResponse> @events = await dbConnection.QueryAsync<TicketTypeResponse>(sql);

        return @events.ToList();
    }
}
