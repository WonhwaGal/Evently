using System.Text.RegularExpressions;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Categories.Categories;

namespace Evently.Modules.Events.Domain.Categories;
public sealed class Category : Entity
{
    /// <summary>
    /// Конструктор по умолчанию (для ORM)
    /// </summary>
    private Category()
    {
    }

    /// <summary>
    /// Создать категорию
    /// </summary>
    /// <param name="name"> Название категории </param>
    /// <returns></returns>
    public static Result<Category> Create(string name)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = name,
            IsArchived = false
        };

        if (category.CheckName(category.Name).IsSuccess)
        {
            category.Raise(new CategoryCreatedDomainEvent(category.Id));
            return category;
        }
        else
        {
            return Result.Failure<Category>(CategoryErrors.IncorrectName);
        }
    }

    /// <summary>
    /// Архивировать категорию
    /// </summary>
    public void Archive()
    {
        IsArchived = true;

        Raise(new CategoryArchivedDomainEvent(Id));
    }

    /// <summary>
    /// Изменить название категории
    /// Идентификатор
    /// </summary>
    /// <param name="name"> Новое название </param>
    public Result ChangeName(string name)
    {
        if (Name == name)
        {
            return Result.Success();
        }

        if (CheckName(name).IsFailure)
        {
            return Result.Failure(CategoryErrors.IncorrectName);
        }

        Name = name;
        Raise(new CategoryNameChangedDomainEvent(Id, Name));
        return Result.Success();
    }

    private Result CheckName(string name)
    {
        if (name.Length == 0 || string.IsNullOrWhiteSpace(name) || Regex.IsMatch(name, @"^[^a-zA-Z]+$"))
        {
            return Result.Failure(CategoryErrors.IncorrectName);
        }
        return Result.Success();
    }

    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Флаг, указывающий, что категория находится в архиве
    /// </summary>
    public bool IsArchived { get; private set; }
}
