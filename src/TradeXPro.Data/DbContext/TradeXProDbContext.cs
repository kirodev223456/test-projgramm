using Microsoft.EntityFrameworkCore;
using TradeXPro.Data.Entities;

namespace TradeXPro.Data.DbContext;

public class TradeXProDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public TradeXProDbContext(DbContextOptions<TradeXProDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<AuthLog> AuthLogs => Set<AuthLog>();
    public DbSet<PriceAlert> PriceAlerts => Set<PriceAlert>();
    public DbSet<WatchlistItem> WatchlistItems => Set<WatchlistItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Email).HasMaxLength(256).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
        });

        modelBuilder.Entity<AuthLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email);
            entity.HasIndex(e => e.Timestamp);
            entity.Property(e => e.Action).HasMaxLength(10).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(256).IsRequired();
            entity.Property(e => e.Result).HasMaxLength(20).IsRequired();
        });

        modelBuilder.Entity<PriceAlert>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.UserId);
            entity.Property(e => e.Symbol).HasMaxLength(10).IsRequired();
            entity.Property(e => e.Condition).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId);
        });

        modelBuilder.Entity<WatchlistItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.Symbol }).IsUnique();
            entity.Property(e => e.Symbol).HasMaxLength(10).IsRequired();
            entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId);
        });
    }
}
