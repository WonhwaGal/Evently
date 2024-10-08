using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Domain.Abstractions;

namespace Evently.Modules.Events.Domain.Category;
public sealed class Category : Entity
{
    /// <summary>
    /// Конструктор по умолчанию (для ORM)
    /// </summary>
    private Category()
    {
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

    /// <summary>
    /// Создать категорию
    /// </summary>
    /// <param name="name"> Название категории </param>
    /// <returns></returns>
    public static Category Create(string name)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = name,
            IsArchived = false
        };

        //category.Raise(new CategoryCreatedDomainEvent(category.Id));

        return category;
    }

    /// <summary>
    /// Архивировать категорию
    /// </summary>
    public void Archive()
    {
        IsArchived = true;

        //Raise(new CategoryArchivedDomainEvent(Id));
    }

    /// <summary>
    /// Изменить название категории
    /// </summary>
    /// <param name="name"> Новое название </param>
    public void ChangeName(string name)
    {
        if (Name == name)
        {
            return;
        }

        Name = name;

        //Raise(new CategoryNameChangedDomainEvent(Id, Name));
    }
}

