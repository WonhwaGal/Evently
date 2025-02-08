using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Modules.Users.Application.Users.GetUser;
using Evently.Modules.Users.Application.Users.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Users.Presentation.Users;

internal static class GetUsers
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users", async (ISender sender) =>
        {
            var query = new GetUsersQuery();
            Result<IReadOnlyList<UserResponse>> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);

        })
        .WithTags(Tags.Users);
    }
}
