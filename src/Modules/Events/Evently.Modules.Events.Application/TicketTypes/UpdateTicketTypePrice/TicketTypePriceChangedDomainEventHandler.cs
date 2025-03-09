using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Domain.TicketTypes.TicketTypes;

namespace Evently.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;
public class TicketTypePriceChangedDomainEventHandler : DomainEventHandler<TicketTypePriceChangedDomainEvent>
{
    public override Task Handle(TicketTypePriceChangedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
