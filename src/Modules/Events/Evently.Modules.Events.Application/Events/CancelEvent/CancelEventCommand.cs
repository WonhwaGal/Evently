using Evently.Modules.Events.Application.Messaging;

namespace Evently.Modules.Events.Application.Events.CancelEvent;
public sealed record CancelEventCommand(Guid EventId) : ICommand;
