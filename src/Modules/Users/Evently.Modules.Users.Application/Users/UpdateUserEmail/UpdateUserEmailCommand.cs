using Evently.Common.Application.Messaging;

namespace Evently.Modules.Users.Application.Users.UpdateUserEmail;
public sealed record UpdateUserEmailCommand(Guid Id, string Email) : ICommand;
