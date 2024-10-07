using Evently.Modules.Events.Application.Messaging;
using Evently.Modules.Events.Domain.Events.Events;

namespace Evently.Modules.Events.Application.Events.RescheduleEvent;

public class EventRescheduledDomainEventHandler : IDomainEventHandler<EventRescheduledDomainEvent>
{
    public Task Handle(EventRescheduledDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
