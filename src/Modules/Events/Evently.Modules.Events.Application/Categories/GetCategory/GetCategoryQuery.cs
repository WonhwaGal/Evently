using Evently.Common.Application.Caching;

namespace Evently.Modules.Events.Application.Categories.GetCategory;
public sealed record GetCategoryQuery(Guid CategoryId) : ICachedQuery<CategoryResponse?>
{
    public string CacheKey => $"category-{CategoryId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(2);
}
