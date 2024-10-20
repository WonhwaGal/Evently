using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Categories.ChangeCategoryName;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Categories;

public static class ChangeCategoryName
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("categories/changeName", async (Guid id, string newName, ISender sender) =>
        {
            var command = new ChangeCategoryNameCommand(id, newName);

            Result result = await sender.Send(command);

            if (result.IsSuccess)
            {
                return Results.Ok();
            }
            else
            {
                return Results.BadRequest(result.Error);
            }

        }).WithTags(Tags.Categories);
    }
}
