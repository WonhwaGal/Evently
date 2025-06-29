using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Common.Presentation.Endpoints;
using Evently.Modules.Users.Application.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Users.Presentation.Users;
public sealed class RegisterUser: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (RegisterUserRequest request, ISender sender) =>
        {
            var command = new RegisterUserCommand(request.Email,
                request.Password,
                request.FirstName,
                request.LastName);

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);

        })
        .AllowAnonymous()
        .WithTags(Tags.Users);
    } 
}
