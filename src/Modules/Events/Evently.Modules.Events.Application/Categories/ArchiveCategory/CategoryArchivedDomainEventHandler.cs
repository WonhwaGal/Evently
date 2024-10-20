using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Domain.Categories.Categories;

namespace Evently.Modules.Events.Application.Categories.ArchiveCategory;
public class CategoryArchivedDomainEventHandler : IDomainEventHandler<CategoryArchivedDomainEvent>
{
    public Task Handle(CategoryArchivedDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
