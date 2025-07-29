using Evently.Common.Application.Messaging;
using Evently.Modules.Attendance.Domain.Attendees.Events;
using Evently.Modules.Attendance.Domain.Events;

namespace Evently.Modules.Attendance.Application.EventStatistics.Projections;

internal sealed class InvalidCheckInAttemptedDomainEventHandlerV2(
    IEventStatisticsRepositoryV2 eventStatisticsRepository)
    : DomainEventHandler<InvalidCheckInAttemptedDomainEvent>
{
    public override async Task Handle(
        InvalidCheckInAttemptedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        EventStatisticsV2 eventStatistics =
            await eventStatisticsRepository.GetAsync(domainEvent.EventId, cancellationToken);

        eventStatistics.InvalidCheckInTickets.Add(new TicketModel
        {
            AttendeeId = domainEvent.AttendeeId,
            EventId = domainEvent.EventId,
            TicketId = domainEvent.TicketId,
            TicketCode = domainEvent.TicketCode
        });

        await eventStatisticsRepository.ReplaceAsync(eventStatistics, cancellationToken);
    }
}
