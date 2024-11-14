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
using Evently.Common.Presentation.ApiResults;

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

            return result.Match(Results.Ok, ApiResults.Problem);

        }).WithTags(Tags.Events);
    }
}
