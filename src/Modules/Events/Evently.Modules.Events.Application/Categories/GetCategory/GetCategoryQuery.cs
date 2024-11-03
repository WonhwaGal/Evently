using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Domain.Categories;

namespace Evently.Modules.Events.Application.Categories.GetCategory;
public sealed record GetCategoryQuery(Guid CategoryId): IQuery<CategoryResponse?>
{
}
