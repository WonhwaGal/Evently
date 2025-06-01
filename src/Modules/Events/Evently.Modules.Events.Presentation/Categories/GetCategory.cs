using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Modules.Events.Application.Categories.GetCategory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Categories;
public static class GetCategory
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("categories/{catId}", async (Guid catId, ISender sender) =>
        {
            var query = new GetCategoryQuery(catId);
            Result<CategoryResponse?> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);

        })
        .AllowAnonymous()//.RequireAuthorization()
        .WithTags(Tags.Categories);
    }
}
