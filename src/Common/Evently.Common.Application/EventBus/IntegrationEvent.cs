using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Common.Application.EventBus;

/// <summary>
/// Базовый класс описывающий интеграционное событие
/// </summary>
public abstract class IntegrationEvent : IIntegrationEvent
{
    /// <summary>
    /// Создать новый экземпляр интеграционного события
    /// </summary>
    /// <param name="id"> Идентификатор события </param>
    /// <param name="occurredOnUtc"> Дата и время возникновения события </param>
    protected IntegrationEvent(Guid id, DateTime occurredOnUtc)
    {
        EventId = id;
        OccurredOnUtc = occurredOnUtc;
    }

    protected IntegrationEvent()
    {
    }

    /// <summary>
    /// Идентификатор события
    /// </summary>
    public Guid EventId { get; init; }

    /// <summary>
    /// Дата и время возникновения события
    /// </summary>
    public DateTime OccurredOnUtc { get; init; }

}

