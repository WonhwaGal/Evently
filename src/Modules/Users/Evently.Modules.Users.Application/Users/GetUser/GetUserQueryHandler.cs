using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Users.Domain.Users;

namespace Evently.Modules.Users.Application.Users.GetUser;
internal sealed class GetUserQueryHandler(
    IDbConnectionFactory connectionFactory) : IQueryHandler<GetUserQuery, UserResponse?>
{
    public async Task<Result<UserResponse?>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection dbConnection = await connectionFactory.OpenConnectionAsync(cancellationToken);

        const string sql = $"""
            SELECT
                id AS {nameof(UserResponse.Id)},
                email AS {nameof(UserResponse.Email)},
                first_name AS {nameof(UserResponse.FirstName)},
                last_name AS {nameof(UserResponse.LastName)}
            FROM
                users.users AS u
            WHERE
                id = @UserId
            """;

        UserResponse? user = await dbConnection.QuerySingleOrDefaultAsync<UserResponse?>(
            sql,
            new
            {
                request.UserId
            });

        return user;
    }
}
