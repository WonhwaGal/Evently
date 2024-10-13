using Evently.Modules.Events.Application.Messaging;
using Evently.Modules.Events.Domain.Categories.Categories;

namespace Evently.Modules.Events.Application.Categories.CreateCategory;
public class CategoryCreatedDomainEventHandler : IDomainEventHandler<CategoryCreatedDomainEvent>
{
    public Task Handle(CategoryCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
