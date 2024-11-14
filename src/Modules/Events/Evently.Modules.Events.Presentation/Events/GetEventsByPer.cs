using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Application.Events.GetEvents;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Common.Presentation.ApiResults;

namespace Evently.Modules.Events.Presentation.Events;
public static class GetEventsByPer
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/getByPer", async(DateTime? StartsAtUtc, DateTime? EndsAtUtc, ISender sender) =>
        {
            var query = new GetEventsByPerQuery(StartsAtUtc, EndsAtUtc);

            Result<IReadOnlyList<EventResponse>> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);

        }).WithTags(Tags.Events);
    }
}
