using Evently.Common.Application.EventBus;
using Evently.Common.Application.Exceptions;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypeById;
using Evently.Modules.Events.Domain.TicketTypes.TicketTypes;
using Evently.Modules.Events.IntegrationEvents;
using Evently.Modules.Ticketing.PublicApi;
using MediatR;

namespace Evently.Modules.Events.Application.TicketTypes.CreateTicketType;

public class TicketTypeCreatedDomainEventHandler(
    ISender sender,
    IEventBus eventBus) : IDomainEventHandler<TicketTypeCreatedDomainEvent>
{
    public async Task Handle(TicketTypeCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Result<TicketTypeResponse?> result = await sender.Send(new GetTicketTypeByIdQuery(notification.TicketTypeId), cancellationToken);
        if (result.IsFailure)
        {
            throw new EventlyException(nameof(GetTicketTypeByIdQuery), result.Error);
        }

        var ticketTypeIntegrationEvent = new TicketTypeCreatedIntegrationEvent(
            notification.DomainEventId,
            notification.OccurredOnUtc,
            notification.TicketTypeId,
            result.Value.EventId,
            result.Value.Name,
            result.Value.Price,
            result.Value.Currency,
            result.Value.Quantity);

        await eventBus.PublishAsync(ticketTypeIntegrationEvent, cancellationToken);
    }
}
