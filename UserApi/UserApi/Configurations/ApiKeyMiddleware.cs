namespace UserApi.Configurations;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "X-API-KEY";
    private readonly IServiceProvider _serviceProvider;

    public ApiKeyMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("API Key is missing");
            return;
        }

        using (var scope = _serviceProvider.CreateScope())
        {
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            if (!await ValidateApiKey(extractedApiKey, userService))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }

            var claimsIdentity = new ClaimsIdentity(claims, "ApiKey");
            context.User = new ClaimsPrincipal(claimsIdentity);
        }

        await _next(context);
    }

    private List<Claim>? claims;

    private async Task<bool> ValidateApiKey(string apiKey, IUserService userService)
    {
        claims = null;
        User? user = await userService.GetUserRolesAsync(apiKey);

        if (user is not null)
        {
            claims =
            [
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Role, "Delete/{id}")
            ];

            if (user.IsSysAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "ADMIN"));

            foreach (UserRole userRole in user.UserRoles ?? [])
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Name));
            }

            return true;
        }

        return false;
    }
}
