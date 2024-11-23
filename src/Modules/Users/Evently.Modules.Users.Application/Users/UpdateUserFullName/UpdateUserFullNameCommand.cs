using Evently.Common.Application.Messaging;

namespace Evently.Modules.Users.Application.Users.UpdateUserFullName;
public sealed record UpdateUserFullNameCommand(Guid Id,
    string FirstName,
    string LastName) : ICommand;
