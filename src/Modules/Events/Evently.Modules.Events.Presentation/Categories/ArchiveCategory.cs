using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Categories.ArchiveCategory;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Common.Presentation.ApiResults;
using Evently.Common.Presentation.Endpoints;

namespace Evently.Modules.Events.Presentation.Categories;
public sealed class ArchiveCategory: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("categories/archive", async (Guid id, ISender sender) =>
        {
            var command = new ArchiveCategoryCommand(id);

            Result result = await sender.Send(command);

            return result.Match(TypedResults.Ok, ApiResults.Problem);

        }).WithTags(Tags.Categories);
    }
}
