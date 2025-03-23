using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Evently.Common.Infrastructure.Inbox;
/// <summary>
/// Конфигурация сущности InboxMessage
/// </summary>
public sealed class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessage>
{
    /// <summary>
    /// Настроить конфигурацию сущности InboxMessage
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<InboxMessage> builder)
    {
        // Сопоставить наименование таблицы
        builder.ToTable("inbox_messages");
        // Сопоставить первичный ключ
        builder.HasKey(o => o.Id);
        // Добавить ограничение на длину поля Content
        builder.Property(o => o.Content).HasMaxLength(2000);
    }
}
