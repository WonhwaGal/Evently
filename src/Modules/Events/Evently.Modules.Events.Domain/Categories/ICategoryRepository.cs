using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evently.Modules.Events.Domain.Categories;
public interface ICategoryRepository
{
    /// <summary>
    /// Получение категории по имени
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение категории по идентификатору
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавление новой категории
    /// </summary>
    /// <param name="category"></param>
    void Insert(Category category);
}
