namespace Evently.Modules.Attendance.Domain.Tickets;

/// <summary>
/// Предоставляет репозиторий для работы с билетами (Ticket)
/// </summary>
public interface ITicketRepository
{
    /// <summary>
    /// Получить билет (Ticket) по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор билета</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns></returns>
    Task<Ticket?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить новый билет (Ticket)
    /// </summary>
    /// <param name="ticket">Билет</param>
    void Insert(Ticket ticket);
}
