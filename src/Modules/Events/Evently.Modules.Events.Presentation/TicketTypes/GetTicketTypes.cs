using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Common.Presentation.Endpoints;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Application.Events.GetEvents;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypeById;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.TicketTypes;
internal sealed class GetTicketTypes: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("ticketTypes", async (ISender sender) =>
        {
            var query = new GetTicketTypesQuery();
            Result<IReadOnlyList<TicketTypeResponse>> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);

        })
        .WithTags(Tags.TicketTypes);
    }
}
