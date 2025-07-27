namespace Evently.Modules.Attendance.Domain.Events;

public sealed class TicketModel
{
    public Guid AttendeeId { get; init; }

    public Guid EventId { get; init; }

    public Guid TicketId { get; init; }

    public string TicketCode { get; init; }
}
