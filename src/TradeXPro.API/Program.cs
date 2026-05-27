using Microsoft.EntityFrameworkCore;
using TradeXPro.API.Services;
using TradeXPro.Data.DbContext;
using TradeXPro.Data.Entities;
using TradeXPro.Data.Repositories.Implementations;
using TradeXPro.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database - MSSQL (swap provider here to change database)
// To switch to PostgreSQL: change UseSqlServer → UseNpgsql + update NuGet package
// To switch to MySQL: change UseSqlServer → UseMySql + update NuGet package
builder.Services.AddDbContext<TradeXProDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories (database abstraction layer)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthLogRepository, AuthLogRepository>();
builder.Services.AddScoped<IRepository<PriceAlert>, Repository<PriceAlert>>();
builder.Services.AddScoped<IRepository<WatchlistItem>, Repository<WatchlistItem>>();

// Services
builder.Services.AddSingleton<IAuthFileLogger, AuthFileLogger>();

// CORS for Web MVC client
builder.Services.AddCors(options =>
{
    options.AddPolicy("WebClient", policy =>
    {
        policy.WithOrigins("http://localhost:5108", "https://localhost:7198")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("WebClient");
app.UseAuthorization();
app.MapControllers();

app.Run();
