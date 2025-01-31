using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Abstractions.Identity;

namespace Evently.Modules.Users.Infrastructure.Identity;
internal sealed class IdentityProviderService(
    /*ILogger<IdentityProviderService> logger*/) : IIdentityProviderService
{
    public Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
