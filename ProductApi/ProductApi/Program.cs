var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "ApiKey";
    options.DefaultChallengeScheme = "ApiKey";
})
.AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", null);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "X-API-KEY",
        Type = SecuritySchemeType.ApiKey,
        Description = "API Key needed to access the endpoints. Add your API Key in the text input below:"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<ProductApiDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("ProductDatabase")));
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<INotifierService, NotifierService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
