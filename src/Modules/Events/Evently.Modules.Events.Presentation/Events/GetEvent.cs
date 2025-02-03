using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Common.Presentation.ApiResults;

namespace Evently.Modules.Events.Presentation.Events;

public static class GetEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetEventQuery(id);
            Result<EventResponse?> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);

        }).WithTags(Tags.Events);
    }
}
