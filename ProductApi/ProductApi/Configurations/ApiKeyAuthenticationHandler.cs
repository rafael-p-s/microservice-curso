namespace ProductApi.Configurations;

public class ApiKeyAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IClientService clientService)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    private const string ApiKeyHeaderName = "X-API-KEY";
    private List<Claim>? claims;
    private const string GET_BY_APIKEY_URL = "https://localhost:7140/User/GetUserRoles/";

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKey))
        {
            return AuthenticateResult.Fail("API Key not found");
        }

        var ticket = await ValidateApiKey(apiKey);

        if (ticket == null)
            return AuthenticateResult.Fail("Invalid API Key");

        return AuthenticateResult.Success(ticket);
    }

    private async Task<AuthenticationTicket?> ValidateApiKey(string? apiKey)
    {
        if (apiKey == null)
            return null;

        claims = null;
        var url = string.Concat(GET_BY_APIKEY_URL, apiKey);
        var responseDto = await clientService.GetAsync(url);

        if (!responseDto?.Status ?? true)
            return null;

        UserDto? user = JsonExtensions.DeserializeCustomResponse<UserDto>(responseDto!.Content!);

        if (user is not null)
        {
            claims =
            [
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            ];

            if (user.IsSysAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "ADMIN"));

            foreach (UserRoleDto userRole in user.UserRoles ?? [])
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Name));
            }

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return ticket;
        }

        return null;
    }
}
