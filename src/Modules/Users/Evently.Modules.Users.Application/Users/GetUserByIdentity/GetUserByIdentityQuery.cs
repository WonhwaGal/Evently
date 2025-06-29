using Evently.Common.Application.Messaging;
using Evently.Modules.Users.Application.Users.GetUserById;

namespace Evently.Modules.Users.Application.Users.GetUserByIdentity;

public sealed record GetUserByIdentityQuery(string IdentityId) : IQuery<UserResponse?>;
