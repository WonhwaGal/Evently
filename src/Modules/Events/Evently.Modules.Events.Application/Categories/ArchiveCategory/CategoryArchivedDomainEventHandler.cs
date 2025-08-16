using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Domain.Categories.Events;

namespace Evently.Modules.Events.Application.Categories.ArchiveCategory;
public class CategoryArchivedDomainEventHandler : DomainEventHandler<CategoryArchivedDomainEvent>
{
    public override Task Handle(CategoryArchivedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
