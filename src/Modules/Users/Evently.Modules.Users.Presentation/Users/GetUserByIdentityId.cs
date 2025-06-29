using System.Security.Claims;
using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Common.Presentation.Endpoints;
using Evently.Modules.Users.Application.Users.GetUserById;
using Evently.Modules.Users.Application.Users.GetUserByIdentity;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Users.Presentation.Users;
public sealed class GetUserByIdentityId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/getByIdentityId", async (ISender sender, ClaimsPrincipal claims) =>
        {
            string identityId = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var query = new GetUserByIdentityQuery(identityId!);

            Result<UserResponse?> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);

        }).WithTags(Tags.Users);
    }
}
