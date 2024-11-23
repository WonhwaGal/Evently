using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Modules.Users.Application.Users.UpdateUserEmail;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Users.Presentation.Users;
public static class UpdateUserEmail
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("users/updateEmail", async (Guid id, string email, ISender sender) =>
        {
            var command = new UpdateUserEmailCommand(id, email);

            Result result = await sender.Send(command);

            return result.Match(TypedResults.Ok, ApiResults.Problem);

        }).WithTags(Tags.Users);
    }
}
