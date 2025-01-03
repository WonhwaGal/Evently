using Evently.Common.Application.Exceptions;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypeById;
using Evently.Modules.Events.Domain.TicketTypes.TicketTypes;
using Evently.Modules.Ticketing.PublicApi;
using MediatR;

namespace Evently.Modules.Events.Application.TicketTypes.CreateTicketType;

public class TicketTypeCreatedDomainEventHandler(
    ISender sender,
    ITicketingApi ticketingApi) : IDomainEventHandler<TicketTypeCreatedDomainEvent>
{
    public async Task Handle(TicketTypeCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Result<TicketTypeResponse?> result = await sender.Send(new GetTicketTypeByIdQuery(notification.TicketTypeId), cancellationToken);
        if (result.IsFailure)
        {
            throw new EventlyException(nameof(GetTicketTypeByIdQuery), result.Error);
        }

        await ticketingApi.CreateTicketTypeAsync(
            result.Value.Id,
            result.Value.EventId,
            result.Value.Name,
            result.Value.Price,
            result.Value.Currency,
            result.Value.Quantity,
            cancellationToken);
    }
}
