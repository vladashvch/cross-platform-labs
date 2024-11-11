using Lab6.Data;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Lab6.Controllers;

var builder = WebApplication.CreateBuilder(args);
Env.Load();

var urlMVC = Env.GetString("URL_MVC");

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins(urlMVC)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var dbProvider = Env.GetString("DB_PROVIDER");
var dbHost = Env.GetString("DB_HOST");
var dbName = Env.GetString("DB_NAME");

string connectionString = null;

switch (dbProvider)
{
    case "SqlServer":
        connectionString = $"Server={dbHost};Database={dbName};Trusted_Connection=True;TrustServerCertificate=True;";
        builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
        break;

    case "Postgres":
        var dbUser = Env.GetString("DB_USER");
        var dbPassword = Env.GetString("DB_PASSWORD");
        connectionString = $"Host={dbHost};Database={dbName};Username={dbUser};Password={dbPassword};";
        builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));
        break;

    case "Sqlite":
        connectionString = $"Data Source={dbHost};";
        builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(connectionString));
        break;

    case "InMemory":
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseInMemoryDatabase("HealthcareDb"));
        break;

    default:
        throw new Exception("Unsupported database type.");
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<Auth0Service>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();