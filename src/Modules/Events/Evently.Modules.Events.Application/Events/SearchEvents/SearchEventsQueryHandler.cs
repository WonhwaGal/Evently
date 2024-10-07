using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Application.Messaging;
using Evently.Modules.Events.Domain.Abstractions;
using MediatR;

namespace Evently.Modules.Events.Application.Events.SearchEvents;
public sealed class SearchEventsQueryHandler(
    IDbConnectionFactory dbConnectionFactory) : IQueryHandler<SearchEventsQuery, SearchEventsResponse>
{
    public async Task<Result<SearchEventsResponse>> Handle(SearchEventsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        DateTime startDate = request.StartDate ?? DateTime.Now.AddYears(-50);
        DateTime endDate = request.EndDate ?? DateTime.MaxValue;

        const string sql =
            $"""
            SELECT
                e.Id AS {nameof(EventResponse.Id)},
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
                e.Starts_at_utc >= @startDate AND
                e.Ends_at_utc <= @endDate
            """;

        IEnumerable<EventResponse> @events = await dbConnection
            .QueryAsync<EventResponse>(
            sql,
            new
            {
                startDate,
                endDate
            });

        int firstRequestedIndex = (request.Page - 1) * request.PageSize;
        int nextPageIndex = request.Page * request.PageSize;
        int totalCount = @events.Count();
        int eventsNumber = 0;

        if (totalCount > 0 && firstRequestedIndex < totalCount)
        {
            eventsNumber = nextPageIndex < totalCount ? request.PageSize : totalCount - firstRequestedIndex;
        }

        List<EventResponse> eventsInPage = @events.ToList().GetRange(firstRequestedIndex, eventsNumber);

        var response = new SearchEventsResponse(
            request.Page,
            request.PageSize,
            eventsNumber,
            eventsInPage);

        return response;
    }
}
