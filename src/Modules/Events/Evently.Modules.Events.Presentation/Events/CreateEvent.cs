using Evently.Modules.Events.Application.Events.CreateEvent;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;

public static class CreateEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events/create", async (CreateEventRequest request, ISender sender) =>
        {
            var command = new CreateEventCommand(
                request.CategoryId,
                request.Title,
                request.Description,
                request.Location,
                request.StartAtUtc,
                request.EndAtUtc);
            
            Result<Guid> result = await sender.Send(command);

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
