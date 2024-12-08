using Evently.Common.Domain;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypeById;
using Evently.Modules.Events.PublicApi;
using MediatR;

namespace Evently.Modules.Events.Infrastructure.PublicApi;
internal sealed class EventsApi(ISender sender) : IEventsApi
{
    public async Task<Modules.Events.PublicApi.TicketTypeResponse?> GetAsync(Guid ticketTypeId, CancellationToken cancellationToken)
    {
        Result<Application.TicketTypes.GetTicketTypeById.TicketTypeResponse?> result = await sender.Send(
            new GetTicketTypeByIdQuery(ticketTypeId), cancellationToken);

        return new Modules.Events.PublicApi.TicketTypeResponse(
            result.Value.Id,
            result.Value.Price,
            result.Value.Quantity,
            result.Value.Currency);
    }
}
