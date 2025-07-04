using Evently.Common.Infrastructure.Inbox;
using Evently.Common.Infrastructure.Outbox;
using Evently.Modules.Attendance.Application.Abstractions.Data;
using Evently.Modules.Attendance.Domain.Attendees;
using Evently.Modules.Attendance.Domain.Events;
using Evently.Modules.Attendance.Domain.Tickets;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Attendance.Infrastructure.Database;

public sealed class AttendanceDbContext(DbContextOptions<AttendanceDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Attendee> Attendees { get; set; }

    internal DbSet<Event> Events { get; set; }

    internal DbSet<Ticket> Tickets { get; set; }
    
    internal DbSet<EventStatistics> EventStatistics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Attendance);
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        //modelBuilder.ApplyConfiguration(new AttendeeConfiguration());
        //modelBuilder.ApplyConfiguration(new EventConfiguration());
        //modelBuilder.ApplyConfiguration(new TicketConfiguration());
    }
}
