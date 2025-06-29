using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Users.GetUserById;

namespace Evently.Modules.Users.Application.Users.GetUserByIdentity;
internal sealed class GetUserByIdentityQueryHandler(
    IDbConnectionFactory connectionFactory) : IQueryHandler<GetUserByIdentityQuery, UserResponse?>
{
    public async Task<Result<UserResponse?>> Handle(GetUserByIdentityQuery request, CancellationToken cancellationToken)
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
                u.identity_id = @IdentityId
            """;

        UserResponse? user = await dbConnection.QuerySingleOrDefaultAsync<UserResponse?>(
            sql,
            new
            {
                request.IdentityId
            });

        return user;
    }
}
