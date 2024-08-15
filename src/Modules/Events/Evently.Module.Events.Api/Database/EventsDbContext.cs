using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Module.Events.Api.Events;
using Microsoft.EntityFrameworkCore;

namespace Evently.Module.Events.Api.Database;
public sealed class EventsDbContext(DbContextOptions<EventsDbContext> options): DbContext(options)
{
    /// <summary>
    /// Мероприятие (концерт, фестиваль, выставка и т.д.)
    /// </summary>
    internal DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Events);
    }
}
