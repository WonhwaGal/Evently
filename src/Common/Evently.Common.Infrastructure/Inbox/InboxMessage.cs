

namespace Evently.Common.Infrastructure.Inbox;
/// <summary>
/// Структура, описывающая входящее сообщение
/// </summary>
public sealed class InboxMessage
{
    /// <summary>
    /// Идентификатор сообщения
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Тип сообщения
    /// </summary>
    public string Type { get; init; }

    /// <summary>
    /// Содержимое сообщения (Json-документ)
    /// </summary>
    public string Content { get; init; }

    /// <summary>
    /// Дата и время возникновения события
    /// </summary>
    public DateTime OccurredOnUtc { get; init; }

    /// <summary>
    /// Дата и время обработки сообщения
    /// </summary>
    public DateTime? ProcessedOnUtc { get; init; }

    /// <summary>
    /// Ошибка при обработке сообщения
    /// </summary>
    public string? Error { get; init; }
}
