using System.Data.Common;
using Dapper;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Common.Application.Data;
using Evently.Modules.Events.Domain.Events;

namespace Evently.Modules.Events.Application.Events.GetEvent;

internal sealed class GetEventQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetEventQuery, EventResponse?>
{
    public async Task<Result<EventResponse?>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        const string sql =
            $"""
            SELECT
                id AS {nameof(EventResponse.Id)},
                category_id AS {nameof(EventResponse.CategoryId)},
                title AS {nameof(EventResponse.Title)},
                description AS {nameof(EventResponse.Description)},
                location AS {nameof(EventResponse.Location)},
                starts_at_utc AS {nameof(EventResponse.StartsAtUtc)},
                ends_at_utc AS {nameof(EventResponse.EndsAtUtc)},
                status AS {nameof(EventResponse.Status)}
            FROM
                events.events
            WHERE
                id = @EventId
            """;

        EventResponse? @event = await dbConnection.QuerySingleOrDefaultAsync<EventResponse>(sql, new
        {
            request.EventId
        });

        return @event;
    }
}
