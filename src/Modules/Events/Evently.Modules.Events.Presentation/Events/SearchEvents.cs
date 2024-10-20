using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Application.Events.GetEvents;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Evently.Modules.Events.Application.Events.SearchEvents;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Evently.Common.Domain;

namespace Evently.Modules.Events.Presentation.Events;
public static class SearchEvents
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/search", async (
            DateTime? startDate,
            DateTime? endDate,
            int page,
            int pageSize,
            ISender sender) =>
        {
            var query = new SearchEventsQuery(
                startDate,
                endDate,
            page,
            pageSize);

            Result<SearchEventsResponse> result = await sender.Send(query);

            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }
            else
            {
                return Results.BadRequest(result.Error);
            }

        }).WithTags(Tags.Events);
    }
}
