using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.Domain.Orders;
using Evently.Modules.Ticketing.Domain.Tickets;
using Evently.Modules.Ticketing.Domain.TicketTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evently.Modules.Ticketing.Infrastructure.Tickets;

internal sealed class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Code).HasMaxLength(60);

        builder.HasIndex(t => t.Code).IsUnique();

        builder.HasOne<Customer>().WithMany().HasForeignKey(t => t.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Order>().WithMany().HasForeignKey(t => t.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Event>().WithMany().HasForeignKey(t => t.EventId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<TicketType>().WithMany().HasForeignKey(t => t.TicketTypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
