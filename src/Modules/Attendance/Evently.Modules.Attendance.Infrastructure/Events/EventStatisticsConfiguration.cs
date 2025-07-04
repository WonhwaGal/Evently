using Evently.Modules.Attendance.Domain.Events;
using Evently.Modules.Attendance.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evently.Modules.Attendance.Infrastructure.Events;

internal sealed class EventStatisticsConfiguration : IEntityTypeConfiguration<EventStatistics>
{
    public void Configure(EntityTypeBuilder<EventStatistics> builder)
    {
        builder.ToTable("event_statistics");

        builder.HasKey(es => es.EventId);

        builder.Property(es => es.EventId).ValueGeneratedNever();

        var converter = new StringCollectionJsonValueConverter();
        var comparer = new CollectionValueComparer<string>();

        builder
            .Property(e => e.DuplicateCheckInTickets)
            .HasConversion(converter)
            .Metadata.SetValueComparer(comparer);

        builder
            .Property(e => e.InvalidCheckInTickets)
            .HasConversion(converter)
            .Metadata.SetValueComparer(comparer);

    }
}
