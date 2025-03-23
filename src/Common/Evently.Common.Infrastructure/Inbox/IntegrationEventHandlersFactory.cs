using System.Collections.Concurrent;
using System.Reflection;
using Evently.Common.Application.EventBus;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Common.Infrastructure.Inbox;
/// <summary>
/// Фабрика, позволяющая получить экземпляры обработчиков интеграционного события
/// по типу события, сборке, в которой находится обработчик, и поставщику сервисов
/// </summary>
public static class IntegrationEventHandlersFactory
{
    private static readonly ConcurrentDictionary<string, Type[]> HandlersDictionary = new();

    /// <summary>
    /// Получить все экземпляры обработчиков интеграционного события
    /// </summary>
    /// <param name="type">Тип интеграционного события</param>
    /// <param name="serviceProvider">Поставщик сервисов</param>
    /// <param name="assembly">Сборка, в которой находятся обработчики интеграционных событий</param>
    /// <returns></returns>
    public static IEnumerable<IIntegrationEventHandler> GetHandlers(
        Type type,
        IServiceProvider serviceProvider,
        Assembly assembly)
    {
        Type[] integrationEventHandlerTypes = HandlersDictionary.GetOrAdd(
            $"{assembly.GetName().Name}-{type.Name}",
            _ =>
            {
                Type[] integrationEventHandlers = assembly.GetTypes()
                    .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler<>).MakeGenericType(type)))
                    .ToArray();

                return integrationEventHandlers;
            });

        List<IIntegrationEventHandler> handlers = [];
        foreach (Type integrationEventHandlerType in integrationEventHandlerTypes)
        {
            object integrationEventHandler = serviceProvider.GetRequiredService(integrationEventHandlerType);
            handlers.Add((integrationEventHandler as IIntegrationEventHandler)!);
        }

        return handlers;
    }
}

