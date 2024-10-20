using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Domain.TicketTypes.TicketTypes;

namespace Evently.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;
public class TicketTypePriceChangedDomainEventHandler : IDomainEventHandler<TicketTypePriceChangedDomainEvent>
{
    public Task Handle(TicketTypePriceChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
