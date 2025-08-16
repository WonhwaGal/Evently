using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Domain.Categories.Events;

namespace Evently.Modules.Events.Application.Categories.ChangeCategoryName;
public class CategoryNameChangedDomainEventHandler : DomainEventHandler<CategoryNameChangedDomainEvent>
{
    public override Task Handle(CategoryNameChangedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
