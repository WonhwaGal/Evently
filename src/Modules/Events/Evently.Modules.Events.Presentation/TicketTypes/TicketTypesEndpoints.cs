using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.TicketTypes;
public static class TicketTypesEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateTicketType.MapEndpoint(app);
        UpdateTicketTypePrice.MapEndpoint(app);
        GetTicketTypesByEvent.MapEndpoint(app);
        GetTicketTypeById.MapEndpoint(app);
        GetTicketTypes.MapEndpoint(app);
    }
}
