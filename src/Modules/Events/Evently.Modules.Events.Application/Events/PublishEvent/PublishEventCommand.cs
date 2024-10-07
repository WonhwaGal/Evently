using Evently.Modules.Events.Application.Messaging;

namespace Evently.Modules.Events.Application.Events.PublishEvent;
public sealed record PublishEventCommand(Guid EventId) : ICommand;
