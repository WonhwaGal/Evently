using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Users.Presentation.Users;
public static class UserEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        RegisterUser.MapEndpoint(app);
        GetUserById.MapEndpoint(app);
        UpdateUserEmail.MapEndpoint(app);
        UpdateUserFullName.MapEndpoint(app);
        GetUsers.MapEndpoint(app);
        LoginUser.MapEndpoint(app);
    }
}
