using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Application.Events.GetEvents;
using Evently.Modules.Events.Domain.Abstractions;
using Evently.Modules.Events.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;
public static class GetEventsByPer
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/getByPer", async(DateTime? StartsAtUtc, DateTime? EndsAtUtc, ISender sender) =>
        {
            var query = new GetEventsByPerQuery(StartsAtUtc, EndsAtUtc);

            Result<IReadOnlyList<EventResponse>> result = await sender.Send(query);

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
