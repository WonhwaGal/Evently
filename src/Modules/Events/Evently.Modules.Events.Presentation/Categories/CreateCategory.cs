using Evently.Modules.Events.Application.Categories.CreateCategory;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Common.Presentation.ApiResults;

namespace Evently.Modules.Events.Presentation.Categories;
public static class CreateCategory
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("categories/create", async (string categoryName, ISender sender) =>
        {
            var command = new CreateCategoryCommand(categoryName);

            Result<Guid> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);

        })
        .AllowAnonymous()
        .WithTags(Tags.Categories);
    }
}
