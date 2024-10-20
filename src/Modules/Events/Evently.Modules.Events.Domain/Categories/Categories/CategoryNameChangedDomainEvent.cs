using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Categories.Categories;
public sealed class CategoryNameChangedDomainEvent(Guid id, string newName) : DomainEvent
{
    public Guid CategoryId { get; } = id;

    public string CategoryNewName { get; } = newName;
}
