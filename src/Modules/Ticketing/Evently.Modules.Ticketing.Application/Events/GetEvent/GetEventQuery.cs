using Evently.Common.Application.Messaging;

namespace Evently.Modules.Ticketing.Application.Events.GetEvent;
public sealed record GetEventQuery(Guid Id) : IQuery<EventResponse>;
