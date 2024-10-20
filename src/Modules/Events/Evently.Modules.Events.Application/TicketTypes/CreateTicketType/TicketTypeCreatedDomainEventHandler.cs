using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Domain.TicketTypes.TicketTypes;

namespace Evently.Modules.Events.Application.TicketTypes.CreateTicketType;

public class TicketTypeCreatedDomainEventHandler : IDomainEventHandler<TicketTypeCreatedDomainEvent>
{
    public Task Handle(TicketTypeCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
