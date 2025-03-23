using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Evently.Common.Infrastructure.Inbox;
/// <summary>
/// Конфигурация сущности InboxMessageConsumer
/// </summary>
public sealed class InboxMessageConsumerConfiguration : IEntityTypeConfiguration<InboxMessageConsumer>
{
    /// <summary>
    /// Настроить конфигурацию сущности InboxMessageConsumer
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<InboxMessageConsumer> builder)
    {
        builder.ToTable("inbox_message_consumers");
        builder.HasKey(o => new { o.InboxMessageId, o.Name });
        builder.Property(o => o.Name).HasMaxLength(500);
    }
}
