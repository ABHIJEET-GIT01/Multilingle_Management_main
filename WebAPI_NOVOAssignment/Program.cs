using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using WebAPI_NOVOAssignment.Data;
using WebAPI_NOVOAssignment.WebAPI_NOVOAssignment.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Enable Unicode characters in JSON responses
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Add Entity Framework with SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Data Source=ABHIJEET;Initial Catalog=WebAPI_NOVO_DB;Integrated Security=True;Encrypt=False";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// Add JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Add Authorization
builder.Services.AddAuthorization();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });

    // Add for localhost Angular app if needed
    //options.AddPolicy("AllowLocalhost", policy =>
    //{
    //    policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
    //          .AllowAnyHeader()
    //          .AllowAnyMethod()
    //          .AllowCredentials();
    //});
});

// Add OpenAPI (Swagger)
builder.Services.AddOpenApi();

// Register custom services
RegisterServices.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    // Apply migrations and seed data
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Scalar UI endpoint - serves at root (/)
app.MapGet("/", async (HttpContext context) =>
{
    var html = @"
<!doctype html>
<html>
  <head>
    <title>WebAPI NOVO - API Explorer</title>
    <meta charset='utf-8'/>
    <meta name='viewport' content='width=device-width, initial-scale=1'/>
    <style>
      body {
        margin: 0;
        padding: 0;
        font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', sans-serif;
        background: #fafafa;
      }
    </style>
  </head>
  <body>
    <script id='api-reference' data-url='/openapi/v1.json' data-configuration='{""hideDownloadButton"": false, ""hideTestRequestButton"": false}'></script>
    <script src='https://cdn.jsdelivr.net/npm/@scalar/api-reference'></script>
  </body>
</html>
";
    context.Response.ContentType = "text/html;charset=utf-8";
    await context.Response.WriteAsync(html);
}).WithName("Root").WithOpenApi().ExcludeFromDescription();

// Direct endpoint to Scalar UI
app.MapGet("/scalar", async (HttpContext context) =>
{
    var html = @"
<!doctype html>
<html>
  <head>
    <title>WebAPI NOVO - API Explorer</title>
    <meta charset='utf-8'/>
    <meta name='viewport' content='width=device-width, initial-scale=1'/>
    <style>
      body {
        margin: 0;
        padding: 0;
        font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', sans-serif;
        background: #fafafa;
      }
    </style>
  </head>
  <body>
    <script id='api-reference' data-url='/openapi/v1.json' data-configuration='{""hideDownloadButton"": false, ""hideTestRequestButton"": false}'></script>
    <script src='https://cdn.jsdelivr.net/npm/@scalar/api-reference'></script>
  </body>
</html>
";
    context.Response.ContentType = "text/html;charset=utf-8";
    await context.Response.WriteAsync(html);
}).WithName("Scalar UI").WithOpenApi().ExcludeFromDescription();

app.Run();
