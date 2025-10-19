using MediatR;

namespace Evently.Common.Domain;

public interface IDomainEvent : INotification
{
    /// <summary>
    /// Идентификатор события
    /// </summary>
    Guid DomainEventId { get; }

    /// <summary>
    /// Время возникновения события
    /// </summary>
    DateTime OccurredOnUtc { get; }
}
