using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Events.GetEvent;
using MediatR;

namespace Evently.Modules.Events.Application.Events.GetEvents;
public sealed class GetEventsByPerQueryHandler(
	IDbConnectionFactory dbConnectionFactory) : IRequestHandler<GetEventsByPerQuery, IReadOnlyList<EventResponse>>
{
	public async Task<IReadOnlyList<EventResponse>> Handle(GetEventsByPerQuery request, CancellationToken cancellationToken)
	{
		await using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

		DateTime startsAt = request.StartsAtUtc ?? DateTime.Now.AddYears(-50);
		DateTime endsAt = request.EndsAtUtc ?? DateTime.MaxValue;

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
                e.Starts_at_utc >= @startsAt AND
                e.Ends_at_utc <= @endsAt
            """;

		IEnumerable<EventResponse> @events = await dbConnection
			.QueryAsync<EventResponse>(
			sql,
			new
			{
				startsAt,
				endsAt
			});

		return @events.ToList();
	}
}
