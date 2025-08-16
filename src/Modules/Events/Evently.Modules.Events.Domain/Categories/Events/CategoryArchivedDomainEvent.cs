using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Categories.Events;
public sealed class CategoryArchivedDomainEvent(Guid id) : DomainEvent
{
    public Guid CategoryId { get; } = id;
}
