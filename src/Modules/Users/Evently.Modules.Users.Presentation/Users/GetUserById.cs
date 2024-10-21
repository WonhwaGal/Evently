using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Users.GetUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Users.Presentation.Users;
public static class GetUserById
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/getById/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetUserQuery(id);

            Result<UserResponse?> result = await sender.Send(query);

            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }
            else
            {
                return Results.BadRequest(result.Error);
            }
        }).WithTags(Tags.Users);
    }
}
