

namespace Evently.Common.Infrastructure.Inbox;
/// <summary>
/// Структура, описывающая потребителя входящего сообщения
/// </summary>
/// <param name="inboxMessageId">Идентификатор входящего сообщения</param>
/// <param name="name">Наименование потребителя</param>
public sealed class InboxMessageConsumer(Guid inboxMessageId, string name)
{
    /// <summary>
    /// Идентификатор входящего сообщения
    /// </summary>
    public Guid InboxMessageId { get; init; } = inboxMessageId;

    /// <summary>
    /// Наименование потребителя
    /// </summary>
    public string Name { get; init; } = name;
}
