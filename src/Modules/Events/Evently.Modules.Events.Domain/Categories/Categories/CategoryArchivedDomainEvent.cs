using Evently.Modules.Events.Domain.Abstractions;

namespace Evently.Modules.Events.Domain.Categories.Categories;
public sealed class CategoryArchivedDomainEvent(Guid id) : DomainEvent
{
    public Guid CategoryId { get; } = id;
}
