using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Categories;
public static class CategoriesEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateCategory.MapEndpoint(app);
        ChangeCategoryName.MapEndpoint(app);
        ArchiveCategory.MapEndpoint(app);
        GetCategory.MapEndpoint(app);
        GetCategories.MapEndpoint(app);
    }
}
