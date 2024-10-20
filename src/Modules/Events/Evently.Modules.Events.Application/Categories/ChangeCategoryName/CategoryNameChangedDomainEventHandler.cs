using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Domain.Categories.Categories;

namespace Evently.Modules.Events.Application.Categories.ChangeCategoryName;
public class CategoryNameChangedDomainEventHandler : IDomainEventHandler<CategoryNameChangedDomainEvent>
{
    public Task Handle(CategoryNameChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
