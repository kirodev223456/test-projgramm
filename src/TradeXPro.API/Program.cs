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

// Seed default admin user on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TradeXProDbContext>();
    db.Database.EnsureCreated();

    if (!db.Users.Any(u => u.Email == "admin@tradexpro.com"))
    {
        db.Users.Add(new User
        {
            Email = "admin@tradexpro.com",
            PasswordHash = "Admin123!", // In production: use proper hashing (BCrypt, etc.)
            FirstName = "John",
            LastName = "Doe",
            Phone = "+1 (555) 234-5678",
            Timezone = "Eastern Time (UTC-5)",
            Currency = "USD",
            AccountType = "Premium",
            TwoFactorEnabled = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        });
        db.SaveChanges();
    }
}

app.Run();
