using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Domain.Categories.Events;

namespace Evently.Modules.Events.Application.Categories.CreateCategory;
public class CategoryCreatedDomainEventHandler : DomainEventHandler<CategoryCreatedDomainEvent>
{
    public override Task Handle(CategoryCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
