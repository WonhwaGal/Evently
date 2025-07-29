using Evently.Common.Application.Messaging;
using Evently.Modules.Attendance.Domain.Attendees.Events;
using Evently.Modules.Attendance.Domain.Events;

namespace Evently.Modules.Attendance.Application.EventStatistics.Projections;

internal sealed class DuplicateCheckInAttemptedDomainEventHandlerV2(IEventStatisticsRepositoryV2 eventStatisticsRepository)
    : DomainEventHandler<DuplicateCheckInAttemptedDomainEvent>
{
    public override async Task Handle(
        DuplicateCheckInAttemptedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        EventStatisticsV2 eventStatistics =
            await eventStatisticsRepository.GetAsync(domainEvent.EventId, cancellationToken);

        eventStatistics.DuplicateCheckInTickets.Add(new TicketModel
        {
            AttendeeId = domainEvent.AttendeeId,
            EventId = domainEvent.EventId,
            TicketId = domainEvent.TicketId,
            TicketCode = domainEvent.TicketCode
        });

        await eventStatisticsRepository.ReplaceAsync(eventStatistics, cancellationToken);
    }
}
