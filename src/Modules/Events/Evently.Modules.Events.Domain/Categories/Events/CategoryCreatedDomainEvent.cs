using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Categories.Events;
public sealed class CategoryCreatedDomainEvent(Guid categoryId) : DomainEvent
{
    public Guid CategoryId { get; } = categoryId;
}
