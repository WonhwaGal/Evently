using Evently.Modules.Events.Application.Messaging;
using MediatR;

namespace Evently.Modules.Events.Application.Events.SearchEvents;
public sealed record SearchEventsQuery(
    DateTime? StartDate,
    DateTime? EndDate,
    int Page,
    int PageSize) : IQuery<SearchEventsResponse>;
