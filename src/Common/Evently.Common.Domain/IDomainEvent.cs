using MediatR;

namespace Evently.Common.Domain;

public interface IDomainEvent : INotification
{
    /// <summary>
    /// Идентификатор события
    /// </summary>
    public Guid DomainEventId { get; }

    /// <summary>
    /// Время возникновения события
    /// </summary>
    public DateTime OccurredOnUtc { get; }
}
