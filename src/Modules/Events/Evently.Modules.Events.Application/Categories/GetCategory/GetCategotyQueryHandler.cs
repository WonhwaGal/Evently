using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Categories;

namespace Evently.Modules.Events.Application.Categories.GetCategory;
internal sealed class GetCategotyQueryHandler(
    IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetCategoryQuery, CategoryResponse?>
{
    public async Task<Result<CategoryResponse?>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        const string sql = $"""
            SELECT
                id AS {nameof(CategoryResponse.Id)},
                name AS {nameof(CategoryResponse.Name)},
                is_archived AS {nameof(CategoryResponse.IsArchived)}
            FROM
                events.categories
            WHERE
                id = @CategoryId
            """;

        CategoryResponse? category = await dbConnection.QuerySingleOrDefaultAsync<CategoryResponse>(
            sql,
            new
            {
                request.CategoryId
            });

        return category;
    }
}
