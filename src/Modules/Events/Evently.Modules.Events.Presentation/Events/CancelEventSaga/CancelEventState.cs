using MassTransit;

namespace Evently.Modules.Events.Presentation.Events.CancelEventSaga;

/// <summary>
/// Состояние отмены события
/// </summary>
public class CancelEventState : SagaStateMachineInstance, ISagaVersion
{
    public Guid CorrelationId { get; set; }

    public int Version { get; set; }

    /// <summary>
    /// Текущее состояние Saga
    /// </summary>
    public string CurrentState { get; set; }

    public int CancellationCompletedStatus { get; set; }

}
