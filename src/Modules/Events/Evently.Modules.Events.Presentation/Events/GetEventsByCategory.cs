using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Application.Events.GetEventsByCategory;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;

public static class GetEventsByCategory
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/getByCategory/{categoryId}", async (Guid categoryId, ISender sender) =>
        {
            var query = new GetEventsByCategoryQuery(categoryId);

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
