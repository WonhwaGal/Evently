using Evently.Common.Application.EventBus;

namespace Evently.Modules.Events.IntegrationEvents;

public sealed class EventCreatedIntegrationEvent : IntegrationEvent
{
    public EventCreatedIntegrationEvent(
         Guid id,
         DateTime occurredOnUtc,
         Guid eventId,
         Guid categotyId,
         string title,
         string description,
         string location,
         DateTime startAtUtc,
         DateTime? endAtUtc) : base(id, occurredOnUtc)
    {
        IntegrationEventId = id;
        OccurredOnUtc = occurredOnUtc;
        EventId = eventId;
        CategoryId = categotyId;
        Title = title;
        Description = description;
        Location = location;
        StartAtUtc = startAtUtc;
        EndAtUtc = endAtUtc;
    }

    public EventCreatedIntegrationEvent() { }

    public Guid EventId { get; init; }
    public Guid CategoryId { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Location { get; init; }
    public DateTime StartAtUtc { get; init; }
    public DateTime? EndAtUtc { get; init; }
}
