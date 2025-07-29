using Evently.Common.Application.Messaging;
using Evently.Modules.Attendance.Domain.Events;
using Evently.Modules.Attendance.Domain.Events.Events;

namespace Evently.Modules.Attendance.Application.EventStatistics.Projections;

/// <summary>
/// Обработчик события создания мероприятия (EventCreatedDomainEvent)
/// </summary>
/// <param name="eventStatisticsRepository"></param>
internal sealed class EventCreatedDomainEventHandlerV2(
    IEventStatisticsRepositoryV2 eventStatisticsRepository)
    : DomainEventHandler<EventCreatedDomainEvent>
{
    public override async Task Handle(
        EventCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventStatisticsRepository.InsertAsync(
            EventStatisticsV2.Create(
                domainEvent.EventId,
                domainEvent.Title,
                domainEvent.Description,
                domainEvent.Location,
                domainEvent.StartsAtUtc,
                domainEvent.EndsAtUtc),
            cancellationToken);
    }
}
