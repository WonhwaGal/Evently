using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Users.GetUserById;

namespace Evently.Modules.Users.Application.Users.GetUsers;
internal sealed class GetUsersQueryHandler(
    IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetUsersQuery, IReadOnlyList<UserResponse>>
{
    public async Task<Result<IReadOnlyList<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync(cancellationToken);

        const string sql = $"""
            SELECT
                id AS {nameof(UserResponse.Id)},
                email AS {nameof(UserResponse.Email)},
                first_name AS {nameof(UserResponse.FirstName)},
                last_name AS {nameof(UserResponse.LastName)}
            FROM
                users.users AS u
        
            """
        ;

        IEnumerable<UserResponse> @events = await dbConnection.QueryAsync<UserResponse>(sql);

        return @events.ToList();
    }
}
