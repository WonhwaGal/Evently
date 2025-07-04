using Evently.Common.Application.Messaging;
using Evently.Modules.Attendance.Application.Abstractions.Data;
using Evently.Modules.Attendance.Domain.Attendees.Events;
using Evently.Modules.Attendance.Domain.Events;

namespace Evently.Modules.Attendance.Application.EventStatistics.Projections;

/// <summary>
/// Обработчик события, возникающего при попытке повторной регистрации участника
/// на мероприятие (DuplicateCheckInAttemptedDomainEvent)
/// </summary>
/// <param name="eventStatisticRepository"></param>
/// <param name="unitOfWork"></param>
internal sealed class DuplicateCheckInAttemptedDomainEventHandler(
    IEventStatisticRepository eventStatisticRepository,
    IUnitOfWork unitOfWork
)
    : DomainEventHandler<DuplicateCheckInAttemptedDomainEvent>
{
    /// <summary>
    /// Обработать событие попытки повторной регистрации участника на мероприятие
    /// </summary>
    /// <param name="domainEvent"></param>
    /// <param name="cancellationToken"></param>
    public override async Task Handle(
        DuplicateCheckInAttemptedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {

        // Получить статистику мероприятия по идентификатору события
        Domain.Events.EventStatistics? eventStatistics = await eventStatisticRepository.GetAsync(domainEvent.EventId, cancellationToken);
        // Добавить в статистику по мероприятию код билета,
        // который был повторно использован для регистрации
        eventStatistics!.DuplicateCheckInTickets.Add(domainEvent.TicketCode);
        // Обновить статистику мероприятия в базе данных
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
