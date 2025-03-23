using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Common.Application.EventBus;
/// <summary>
/// Интерфейс описывающий контракт передачи сообщений между модулями
/// Событие интеграции
/// </summary>
public interface IEventBus
{
    /// <summary>
    /// Опубликовать интеграционное событие
    /// </summary>
    /// <param name="integrationEvent"> Интеграционное событие </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <typeparam name="TIntegrationEvent"> Тип интеграционного события </typeparam>
    /// <returns></returns>
    Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
        where TIntegrationEvent : IntegrationEvent;

}
