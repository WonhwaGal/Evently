using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Ticketing.Domain.Events;

namespace Evently.Modules.Ticketing.Application.Events.GetEvent;

internal sealed class GetEventQueryHandler(
    IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetEventQuery, EventResponse>
{
    public async Task<Result<EventResponse>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        const string sql =
            $"""
             SELECT
                 id AS {nameof(EventResponse.EventId)},
                 category_id AS {nameof(EventResponse.CategoryId)},
                 title AS {nameof(EventResponse.Title)},
                 description AS {nameof(EventResponse.Description)},
                 location AS {nameof(EventResponse.Location)},
                 starts_at_utc AS {nameof(EventResponse.StartsAt)},
                 ends_at_utc AS {nameof(EventResponse.EndsAt)}
             FROM ticketing.events
             WHERE id = @Id
             """;

        EventResponse? @event = await connection.QuerySingleOrDefaultAsync<EventResponse>(sql, request);

        if (@event is null)
        {
            return Result.Failure<EventResponse>(EventErrors.NotFound(request.Id));
        }

        return @event;
    }
}
