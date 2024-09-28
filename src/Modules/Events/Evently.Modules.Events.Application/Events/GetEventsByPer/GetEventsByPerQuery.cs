using Evently.Modules.Events.Application.Events.GetEvent;
using MediatR;

namespace Evently.Modules.Events.Application.Events.GetEvents;
public sealed record GetEventsByPerQuery(
    DateTime? StartsAtUtc,
    DateTime? EndsAtUtc) : IRequest<IReadOnlyList<EventResponse>>;
