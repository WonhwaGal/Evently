using Evently.Common.Application.Messaging;

namespace Evently.Modules.Ticketing.Application.Events.CreateEvent;
public sealed record CreateEventCommand : ICommand
{
    public Guid EventId { get; init; }

    public Guid CategoryId { get; init; }

    public string Title { get; init; }

    public string Description { get; init; }

    public string Location { get; init; }

    public DateTime StartsAtUtc { get; init; }

    public DateTime? EndsAtUtc { get; init; }

    private CreateEventCommand() { }

    public CreateEventCommand(Guid eventId,
        Guid categotyId,
        string title,
        string description,
        string location,
        DateTime startAtUtc,
        DateTime? endAtUtc)
    {
        EventId = eventId;
        CategoryId = categotyId;
        Title = title;
        Description = description;
        Location = location;
        StartsAtUtc = startAtUtc;
        EndsAtUtc = endAtUtc;
    }
}
