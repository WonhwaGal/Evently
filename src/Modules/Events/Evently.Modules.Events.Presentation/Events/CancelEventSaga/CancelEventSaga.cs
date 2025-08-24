using Evently.Modules.Events.IntegrationEvents;
using Evently.Modules.Ticketing.IntegrationEvents;
using MassTransit;

namespace Evently.Modules.Events.Presentation.Events.CancelEventSaga;

/// <summary>
/// Сага об отмене мероприятия "Event"
/// </summary>
public sealed class CancelEventSaga : MassTransitStateMachine<CancelEventState>
{
    /// <summary>
    /// Промежуточное состояние (общее)
    /// </summary>
    public State CancellationStarted { get; private set; }

    /// <summary>
    /// Промежуточное состояние для операции возврата платежей
    /// </summary>
    public State PaymentsRefunded { get; private set; }

    /// <summary>
    /// Промежуточное состояние для операции архивации билетов
    /// </summary>
    public State TicketsArchived { get; private set; }

    /// <summary>
    /// Интеграционное событие, описывающее отмену мероприятия,
    /// положит начало саге
    /// </summary>
    public Event<EventCanceledIntegrationEvent> EventCanceled { get; private set; }

    /// <summary>
    /// Интеграционное событие, описывающее возврат платежей
    /// </summary>
    public Event<EventPaymentsRefundedIntegrationEvent> EventPaymentsRefunded { get; private set; }

    /// <summary>
    /// Интеграционное событие описывающее архивацию билетов
    /// </summary>
    public Event<EventTicketsArchivedIntegrationEvent> EventTicketsArchived { get; private set; }

    /// <summary>
    /// Событие завершение операции отмены мероприятия
    /// </summary>
    public Event EventCancellationCompleted { get; private set; }

    public CancelEventSaga()
    {
        Event(() => EventCanceled, c => c.CorrelateById(m => m.Message.EventId));
        Event(() => EventPaymentsRefunded, c => c.CorrelateById(m => m.Message.EventId));
        Event(() => EventTicketsArchived, c => c.CorrelateById(m => m.Message.EventId));

        // Указать на свойство состояния, которое будет хранить текущее состояние саги
        InstanceState(s => s.CurrentState);

        Initially( // Начальное состояние саги
            When(EventCanceled) // Указать на событие, которое начнет сагу
                .Publish(context => new EventCancellationStartedIntegrationEvent(
                    context.Message.EventId,
                    context.Message.OccurredOnUtc,
                    context.Message.EventId
                ))
                .TransitionTo(CancellationStarted)); // Перейти в состояние "Отмена начата"

        During(CancellationStarted,  // Промежуточное состояние "Отмена начата"
            When(EventPaymentsRefunded) // Если в текущем состоянии пришло событие возврата платежей
                .TransitionTo(PaymentsRefunded), // Перевожу в состояние "Платежи возвращены"
            When(EventTicketsArchived) // Если в текущем состоянии пришло событие архивации билетов
                .TransitionTo(TicketsArchived) // Перевожу в состояние "Билеты архивированы"
            );

        During(PaymentsRefunded, // Промежуточное состояние "Платежи возвращены"
            When(EventTicketsArchived) // Если в текущем состоянии пришло событие "Билеты архивированы", то
                .TransitionTo(TicketsArchived)); // перевести конечный автомат в состояние "Билеты архивированы"

        During(TicketsArchived, // Промежуточное состояние "Билеты архивированы"
            When(EventPaymentsRefunded) // Если в текущем состоянии пришло событие "Платежи возвращены", то
                .TransitionTo(PaymentsRefunded)); // перевести конечный автомат в состояние "Платежи возвращены"


        CompositeEvent( // Составное событие
            () => EventCancellationCompleted, // Экземпляр составного события
            state => state.CancellationCompletedStatus,
            EventPaymentsRefunded, EventTicketsArchived);

        DuringAny(
            When(EventCancellationCompleted)
                .Publish(context => new EventCancellationCompletedIntegrationEvent(
                    Guid.NewGuid(),
                    DateTime.UtcNow,
                    context.Saga.CorrelationId
                ))
                .Finalize()); // Завершить сагу
    }
}

