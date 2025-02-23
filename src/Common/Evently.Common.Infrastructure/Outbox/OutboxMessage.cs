using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Common.Infrastructure.Outbox;
/// <summary>
/// Структура, описывающая сообщение в Outbox
/// </summary>
public sealed class OutboxMessage
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
    /// Содержимое сообщения
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
