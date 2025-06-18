namespace Evently.Modules.Attendance.Domain.Attendees;

/// <summary>
/// Представляет репозиторий для работы с посетителями (Attendee)
/// мероприятия (Event)
/// </summary>
public interface IAttendeeRepository
{
    /// <summary>
    /// Получить посетителя (Attendee) по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор посетителя</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns></returns>
    Task<Attendee?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить нового посетителя (Attendee)
    /// </summary>
    /// <param name="attendee">Посетитель мероприятия</param>
    void Insert(Attendee attendee);
}
