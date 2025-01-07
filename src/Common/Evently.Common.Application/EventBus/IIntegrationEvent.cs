

namespace Evently.Common.Application.EventBus;

/// <summary>
/// Интеграционное событие
/// </summary>
public interface IIntegrationEvent
{
    /// <summary>
    /// Идентификатор события
    /// </summary>
    Guid IntegrationEventId { get; init; }

    /// <summary>
    /// Дата и время возникновения события
    /// </summary>
    DateTime OccurredOnUtc { get; }

}
