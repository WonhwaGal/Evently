using Evently.Common.Application.Messaging;

namespace Evently.Modules.Events.Application.TicketTypes.GetByEvent;
public sealed record GetTicketTypesByEventQuery(
    Guid EventId) : IQuery<IReadOnlyList<TicketTypeResponse>>;
