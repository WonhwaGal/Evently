using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.Domain.TicketTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evently.Modules.Ticketing.Infrastructure.TicketTypes;
internal sealed class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
{
    public void Configure(EntityTypeBuilder<TicketType> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).HasMaxLength(25);
        builder.Property(t => t.Price);
        builder.Property(t => t.Quantity);
        builder.Property(t => t.Currency);

        builder.HasOne<Event>().WithMany().HasForeignKey(t => t.EventId);
    }
}
