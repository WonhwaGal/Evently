using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Application.Messaging;
using MediatR;

namespace Evently.Modules.Events.Application.Events.GetEvents;
public sealed record GetEventsByPerQuery(
    DateTime? StartsAtUtc,
    DateTime? EndsAtUtc) : IQuery<IReadOnlyList<EventResponse>>;
