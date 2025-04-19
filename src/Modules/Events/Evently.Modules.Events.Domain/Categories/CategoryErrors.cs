using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Categories;
public static class CategoryErrors
{
    public static Error AlreadyExists(string name) =>
        Error.Conflict("Category.AlreadyExists", $"Category with the name {name} already exists");

    public static Error IsArchived(Guid categoryId) =>
        Error.Conflict(
            "Category.IsArchived", 
            $"Category with the identifier {categoryId} cannot be used due to \"Archived\" status");

    public static Error NotFound(Guid categoryId) =>
        Error.NotFound("Category.NoFound", $"Category with the identifier {categoryId} was not found");

    public static Error IncorrectName =>
        Error.NotFound("Category.IncorrectName", $"Category name should have letters");
}
