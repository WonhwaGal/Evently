using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Events.GetEvent;

namespace Evently.Modules.Events.Application.Events.GetEvents;
internal sealed class GetEventsQueryHandler(
    IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetEventsQuery, IReadOnlyList<EventResponse>>
{
    public async Task<Result<IReadOnlyList<EventResponse>>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        const string sql =
        $"""
            SELECT
                e.Id AS {nameof(EventResponse.Id)},
                e.Title AS {nameof(EventResponse.Title)},
                e.Category_id As {nameof(EventResponse.CategoryId)},
                e.Description AS {nameof(EventResponse.Description)},
                e.Location AS {nameof(EventResponse.Location)},
                e.Starts_at_utc AS {nameof(EventResponse.StartsAtUtc)},
                e.Ends_at_utc AS {nameof(EventResponse.EndsAtUtc)},
                e.Status AS {nameof(EventResponse.Status)}
            FROM
                events.events AS e
            """;

        IEnumerable<EventResponse> events = await dbConnection.QueryAsync<EventResponse>(sql);

        return events.ToList();
    }
}
