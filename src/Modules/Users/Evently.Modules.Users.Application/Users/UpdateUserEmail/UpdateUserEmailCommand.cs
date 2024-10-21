using Evently.Common.Application.Messaging;

namespace Evently.Modules.Users.Application.Users.UpdateUser;
public sealed record UpdateUserEmailCommand(Guid Id, string Email) : ICommand;
