using System.Data.Common;
using Dapper;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Common.Application.Data;

namespace Evently.Modules.Events.Application.Events.GetEventsByCategory;
internal sealed class GetEventsByCategoryQueryHandler(
    IDbConnectionFactory dbConnectionFactory
    ) : IQueryHandler<GetEventsByCategoryQuery, IReadOnlyList<EventResponse>>
{
    public async Task<Result<IReadOnlyList<EventResponse>>> Handle(GetEventsByCategoryQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        Guid categoryId = request.CategoryId;

        const string sql = 
            $"""
            SELECT
                e.Id AS {nameof(EventResponse.Id)},
                e.Category_id As {nameof(EventResponse.CategoryId)},
                e.Title AS {nameof(EventResponse.Title)},
                e.Description AS {nameof(EventResponse.Description)},
                e.Location AS {nameof(EventResponse.Location)},
                e.Starts_at_utc AS {nameof(EventResponse.StartsAtUtc)},
                e.Ends_at_utc AS {nameof(EventResponse.EndsAtUtc)},
                e.Status AS {nameof(EventResponse.Status)}
            FROM
                events.events AS e
            WHERE
                e.Status = 1 AND
                e.Category_id = @categoryId
            """;

        IEnumerable<EventResponse> @events = await dbConnection
            .QueryAsync<EventResponse>(
            sql,
            new
            {
                categoryId
            });

        return events.ToList();
    }
}
