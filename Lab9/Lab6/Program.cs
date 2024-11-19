using Lab6.Data;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Lab6.Controllers;
using Lab6.Service;
using Lab6.Models;

var builder = WebApplication.CreateBuilder(args);
Env.Load();

var urlMVC = Env.GetString("URL_MVC");

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var dbName = Env.GetString("DB_NAME");
var dbProvider = Env.GetString("DB_PROVIDER");

var dbHost = Env.GetString("DB_HOST");
var dbPort = Env.GetString("DB_PORT");
var dbUser = Env.GetString("DB_USER");
var dbPassword = Env.GetString("DB_PASSWORD");

string connectionString = null;

switch (dbProvider)
{
    case "SqlServer":
        connectionString = $"Server={dbHost},{dbPort};Database={dbName};User Id={dbUser};Password={dbPassword};TrustServerCertificate=True;";
        builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
        break;

    case "Postgres":
        connectionString = $"Host={dbHost},{dbPort};Database={dbName};Username={dbUser};Password={dbPassword};";
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
builder.Services.AddScoped<TokenVerificationService>();


var app = builder.Build();


void ApplyMigrations(IServiceProvider serviceProvider)
{
    using (var scope = serviceProvider.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        context.Database.EnsureCreated();
        context.Database.Migrate();
    }
}

switch (dbProvider)
{
    case "SqlServer":
    case "Postgres":
    case "Sqlite":
        ApplyMigrations(builder.Services.BuildServiceProvider());
        break;

    case "InMemory":
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            context.Database.EnsureCreated();
        }
        break;

    default:
        throw new Exception("Unsupported database type.");
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors("AllowAnyOrigin");

app.MapControllers();

app.Run();