using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Users.GetUserById;
using Evently.Modules.Users.PublicApi;
using MediatR;

namespace Evently.Modules.Users.Infrastructure.PublicApi;
internal sealed class UsersApi(ISender sender) : IUsersApi
{
    public async Task<Modules.Users.PublicApi.UserResponse?> GetAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        Result<Application.Users.GetUserById.UserResponse?> result =
            await sender.Send(new GetUserByIdQuery(userId), cancellationToken);

        //TODO: Исправить через использование AutoMapper
        return new Modules.Users.PublicApi.UserResponse(
            result.Value.Id,
            result.Value.Email,
            result.Value.FirstName,
            result.Value.LastName
        );
    }
}
