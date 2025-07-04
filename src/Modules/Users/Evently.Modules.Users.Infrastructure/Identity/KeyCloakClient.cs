using System.Net.Http.Json;
using System.Text;
using Evently.Modules.Users.Infrastructure.Identity.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Evently.Modules.Users.Infrastructure.Identity;
internal sealed class KeyCloakClient(
    HttpClient httpClient,
    IOptions<KeyCloakOptions> options)
{
    internal async Task<string> RegisterUserAsync(UserRepresentation user, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(
            "users",
            user,
            cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();

        return ExtractIdentityIdFromLocationHeader(httpResponseMessage);
    }

    internal async Task<string> GetClientId(CancellationToken cancellationToken = default)
    {
        HttpResponseMessage clientResp = await httpClient.GetAsync($"clients?clientId={options.Value.ConfidentialClientId}", cancellationToken);
        clientResp.EnsureSuccessStatusCode();
        string clientJson = await clientResp.Content.ReadAsStringAsync(cancellationToken);
        List<KeycloakClientRepresentation> clients = JsonConvert.DeserializeObject<List<KeycloakClientRepresentation>>(clientJson);

        //if(!clientJson.Any())
        return clients![0].Id;
    }

    internal async Task<RoleRepresentation> GetRoleId(string roleName, string clientId, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage roleResponse = await httpClient.GetAsync($"clients/{clientId}/roles/manager", cancellationToken);
        roleResponse.EnsureSuccessStatusCode();

        string roleJson = await roleResponse.Content.ReadAsStringAsync(cancellationToken);
        RoleRepresentation? role = JsonConvert.DeserializeObject<RoleRepresentation>(roleJson);

        return role!;
    }

    internal async Task<bool> AssignRoleToUser(RoleRepresentation role, string userIdentityId, string clientId, CancellationToken cancellationToken = default)
    {
        using var jsonContent = new StringContent(JsonConvert.SerializeObject(new []{ role } ), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await httpClient.PostAsync($"users/{userIdentityId}/role-mappings/clients/{clientId}", jsonContent, cancellationToken);
        response.EnsureSuccessStatusCode();

        return false;
    }

    private static string ExtractIdentityIdFromLocationHeader(
        HttpResponseMessage httpResponseMessage)
    {
        const string usersSegmentName = "users/";

        string? locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location header is null");
        }

        int userSegmentValueIndex = locationHeader.IndexOf(
            usersSegmentName,
            StringComparison.InvariantCultureIgnoreCase);

        //string identityId = locationHeader.Substring(userSegmentValueIndex + usersSegmentName.Length);
        string identityId = locationHeader[(userSegmentValueIndex + usersSegmentName.Length)..];

        return identityId;
    }
}
