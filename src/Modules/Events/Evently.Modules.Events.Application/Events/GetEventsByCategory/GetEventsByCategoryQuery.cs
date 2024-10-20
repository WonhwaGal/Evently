using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Common.Application.Messaging;

namespace Evently.Modules.Events.Application.Events.GetEventsByCategory;
public sealed record GetEventsByCategoryQuery(Guid CategoryId) : IQuery<IReadOnlyList<EventResponse>>;
