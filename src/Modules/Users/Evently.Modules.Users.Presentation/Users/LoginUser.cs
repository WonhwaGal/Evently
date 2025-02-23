using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Modules.Users.Application.Users.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Users.Presentation.Users;

internal sealed class LoginUser
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async (Request request, ISender sender) =>
        {
            Result<AccessTokenResponse> result = await sender.Send(new LogInUserCommand(
                request.Email,
                request.Password));
            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .AllowAnonymous()
            .WithTags(Tags.Users);
    }

    internal sealed class Request
    {
        public string Email { get; init; }

        public string Password { get; init; }

    }
}
