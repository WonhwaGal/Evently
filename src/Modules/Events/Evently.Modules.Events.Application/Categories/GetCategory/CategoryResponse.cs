using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Application.Categories.GetCategory;
public sealed class CategoryResponse
{
    /// <summary>
    /// Идентификатор категории мероприятия
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование категории
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Флаг, указывающий, что категория находится в архиве
    /// </summary>
    public bool IsArchived { get; set; }
}
