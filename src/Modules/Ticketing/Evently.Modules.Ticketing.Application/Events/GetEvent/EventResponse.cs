
namespace Evently.Modules.Ticketing.Application.Events.GetEvent;
public sealed record EventResponse(
    Guid EventId,
    Guid CategoryId,
    string Title,
    string Description,
    string Location,
    DateTime StartsAt,
    DateTime? EndsAt);
