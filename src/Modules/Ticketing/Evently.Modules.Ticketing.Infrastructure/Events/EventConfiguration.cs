using Evently.Modules.Ticketing.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evently.Modules.Ticketing.Infrastructure.Events;
internal sealed class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.CategoryId);
        builder.Property(e => e.Title).HasMaxLength(25);
        builder.Property(e => e.Description).HasMaxLength(200);
        builder.Property(e => e.Location).HasMaxLength(80);
        builder.Property(e => e.StartsAtUtc);
        builder.Property(e => e.EndsAtUtc);
    }
}
