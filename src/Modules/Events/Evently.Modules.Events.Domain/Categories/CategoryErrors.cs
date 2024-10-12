using Evently.Modules.Events.Domain.Abstractions;

namespace Evently.Modules.Events.Domain.Categories;
public static class CategoryErrors
{
    public static Error AlreadyExists(string name) =>
        Error.Conflict("Category.AlreadyExists", $"Category with the name {name} already exists");

    public static Error NotFound(Guid categoryId) =>
        Error.NotFound("Category.NoFound", $"Category with the identifier {categoryId} was not found");
}
