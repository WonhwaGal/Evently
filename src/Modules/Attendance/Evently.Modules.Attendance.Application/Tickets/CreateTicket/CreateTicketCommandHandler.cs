using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Attendance.Application.Abstractions.Data;
using Evently.Modules.Attendance.Domain.Attendees;
using Evently.Modules.Attendance.Domain.Events;
using Evently.Modules.Attendance.Domain.Tickets;

namespace Evently.Modules.Attendance.Application.Tickets.CreateTicket;

/// <summary>
/// Обработчик команды для создания билета на мероприятие
/// </summary>
/// <param name="attendeeRepository"></param>
/// <param name="eventRepository"></param>
/// <param name="ticketRepository"></param>
/// <param name="unitOfWork"></param>
internal sealed class CreateTicketCommandHandler(
    IAttendeeRepository attendeeRepository,
    IEventRepository eventRepository,
    ITicketRepository ticketRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateTicketCommand>
{
    /// <summary>
    /// Обработать команду для создания билета на мероприятие
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        // Проверить существование посетителя
        Attendee? attendee = await attendeeRepository.GetAsync(request.AttendeeId, cancellationToken);

        // Если посетитель не найден, вернуть ошибку
        if (attendee is null)
        {
            return Result.Failure(AttendeeErrors.NotFound(request.AttendeeId));
        }

        // Проверить существование мероприятия
        Event? @event = await eventRepository.GetAsync(request.EventId, cancellationToken);

        // Если мероприятие не найдено, вернуть ошибку
        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        // Создать новый билет
        var ticket = Ticket.Create(request.TicketId, attendee, @event, request.Code);
        
        ticketRepository.Insert(ticket);
        
        // Сохранить изменения в базе данных
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Возвратить успешный результат
        return Result.Success();
    }
}
