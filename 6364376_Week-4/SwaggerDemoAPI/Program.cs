using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SwaggerDemoAPI.Filters; // Import your custom exception filter

var builder = WebApplication.CreateBuilder(args);

// 🔐 JWT Authentication Setup
string securityKey = "mysuperdupersecretkey@1234567890!@#$"; // ✅ Secure key (32+ chars)
var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "mySystem",
        ValidAudience = "myUsers",
        IssuerSigningKey = symmetricSecurityKey
    };
});

// ✅ Add Controllers + Global Exception Filter
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});

// ✅ Swagger Setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Swagger Demo",
        Version = "v1",
        Description = "Minimal + Controller API Swagger Integration with JWT",
        Contact = new OpenApiContact
        {
            Name = "Adarsh",
            Email = "adarsh@example.com",
            Url = new Uri("https://example.com")
        }
    });
});

var app = builder.Build();

// ✅ Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo v1");
    });
}

// 🔐 Enable JWT Auth Middleware
app.UseAuthentication(); // Must come before UseAuthorization
app.UseAuthorization();

// ✅ Map API Controllers
app.MapControllers();

app.Run();
