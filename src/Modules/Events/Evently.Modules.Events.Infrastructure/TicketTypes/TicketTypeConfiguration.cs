using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Domain.TicketTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evently.Modules.Events.Infrastructure.TicketTypes;
internal sealed class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
{
    public void Configure(EntityTypeBuilder<TicketType> builder)
    {
        builder.ToTable("ticket_types");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name);
        builder.Property(t => t.Price).HasColumnType("decimal(10,2)");
        builder.Property(t => t.Currency);
        builder.Property(t => t.Quantity).HasColumnType("decimal(18,0)");

        builder.HasOne<Event>().WithMany().HasForeignKey(t => t.EventId);
    }
}
