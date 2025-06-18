using Evently.Common.Domain;
using Evently.Modules.Attendance.Domain.Attendees.Events;
using Evently.Modules.Attendance.Domain.Tickets;

namespace Evently.Modules.Attendance.Domain.Attendees;

/// <summary>
/// Участник мероприятия (Attendee) представляет собой сущность,
/// которая содержит информацию о человеке, зарегистрированном на мероприятие (Event)
/// </summary>
public sealed class Attendee : Entity
{
    /// <summary>
    /// Конструктор по умолчанию (для EF Core)
    /// </summary>
    private Attendee()
    {
    }

    /// <summary>
    /// Идентификатор участника мероприятия
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Статический метод для создания нового участника мероприятия
    /// </summary>
    /// <param name="id"></param>
    /// <param name="email"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <returns></returns>
    public static Attendee Create(Guid id, string email, string firstName, string lastName)
    {
        return new Attendee
        {
            Id = id,
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };
    }

    /// <summary>
    /// Обновить информацию об участнике мероприятия
    /// </summary>
    /// <param name="firstName">Имя</param>
    /// <param name="lastName">Фамилия</param>
    public void Update(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>
    /// Зарегистрировать участника на мероприятие
    /// </summary>
    /// <param name="ticket">Билет</param>
    /// <returns></returns>
    public Result CheckIn(Ticket ticket)
    {
        if (Id != ticket.AttendeeId)
        {
            // Если идентификатор участника не совпадает с идентификатором владельца билета,
            // то это недопустимая попытка регистрации на мероприятие
            Raise(new InvalidCheckInAttemptedDomainEvent(Id, ticket.EventId, ticket.Id, ticket.Code));
            // Вернуть ошибку
            return Result.Failure(TicketErrors.InvalidCheckIn);
        }
        
        if (ticket.UsedAtUtc.HasValue)
        {
            // Если билет уже использован, то это дублирующая попытка регистрации
            // на мероприятие
            Raise(new DuplicateCheckInAttemptedDomainEvent(Id, ticket.EventId, ticket.Id, ticket.Code));
            // Вернуть ошибку
            return Result.Failure(TicketErrors.DuplicateCheckIn);
        }
    
        // Пометить билет как использованный
        ticket.MarkAsUsed();

        // Возбудить доменное событие о регистрации участника на мероприятии
        Raise(new AttendeeCheckedInDomainEvent(Id, ticket.EventId));

        // Вернуть успешный результат
        return Result.Success();
    }
}
