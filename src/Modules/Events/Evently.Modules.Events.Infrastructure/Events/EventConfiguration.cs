
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Domain.Category;
using Evently.Modules.Events.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evently.Modules.Events.Infrastructure.Events;
internal sealed class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("events");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title);
        builder.Property(e => e.Description).HasMaxLength(2000);
        builder.Property(e => e.Location);
        builder.Property(e => e.StartsAtUtc);
        builder.Property(e => e.EndsAtUtc);
        builder.Property(e => e.Status);
        builder.HasOne<Category>().WithMany().HasForeignKey(e => e.CategoryId);
    }
}
