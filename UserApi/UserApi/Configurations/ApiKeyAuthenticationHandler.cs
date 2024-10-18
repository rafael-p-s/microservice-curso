namespace UserApi.Configurations;

public class ApiKeyAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IUserService userService)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    private const string ApiKeyHeaderName = "X-API-KEY";
    private List<Claim>? claims;

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
        User? user = await userService.GetUserRolesAsync(apiKey);

        if (user is not null)
        {
            claims =
            [
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            ];

            if (user.IsSysAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "ADMIN"));

            foreach (UserRole userRole in user.UserRoles ?? [])
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
