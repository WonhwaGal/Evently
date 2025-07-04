using Evently.Modules.Attendance.Domain.Events;
using Evently.Modules.Attendance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Attendance.Infrastructure.Events;

public class EventStatisticRepository(AttendanceDbContext context) : IEventStatisticRepository
{
    public async Task<EventStatistics?> GetAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return await context.EventStatistics.SingleOrDefaultAsync(e => e.EventId == eventId, cancellationToken);
    }

    public void Insert(EventStatistics eventStatistics)
    {
        context.EventStatistics.Add(@eventStatistics);
    }
}

