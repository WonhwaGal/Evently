using Evently.Common.Application.Messaging;
using Evently.Modules.Attendance.Application.Abstractions.Data;
using Evently.Modules.Attendance.Domain.Attendees.Events;
using Evently.Modules.Attendance.Domain.Events;

namespace Evently.Modules.Attendance.Application.EventStatistics.Projections;

/// <summary>
/// Обработчик события, возникающего при попытке невалидной
/// регистрации участника на мероприятие
/// </summary>
/// <param name="eventStatisticRepository">Репозиторий для работы со статистикой мероприятий</param>
/// <param name="unitOfWork">Для сохранения изменений в бд</param>

internal sealed class InvalidCheckInAttemptedDomainEventHandler(
    IEventStatisticRepository eventStatisticRepository,
    IUnitOfWork unitOfWork)
    : DomainEventHandler<InvalidCheckInAttemptedDomainEvent>
{
    /// <summary>
    /// Обработать событие попытки невалидной регистрации
    /// участника на мероприятие
    /// </summary>
    /// <param name="domainEvent">Событие попытки невалидной регистрации участника на мероприятие</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public override async Task Handle(
        InvalidCheckInAttemptedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        // Получить статистику мероприятия по идентификатору события
        Domain.Events.EventStatistics? eventStatistics = await eventStatisticRepository.GetAsync(domainEvent.EventId, cancellationToken);
        // Добавить в статистику по мероприятию код билета,
        // который был использован при невалидной регистрации участника
        // на мероприятие
        eventStatistics!.InvalidCheckInTickets.Add(domainEvent.TicketCode);
        // Обновить статистику мероприятия в базе данных
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
