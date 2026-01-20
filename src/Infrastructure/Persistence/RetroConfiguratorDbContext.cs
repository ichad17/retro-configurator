using Microsoft.EntityFrameworkCore;
using RetroConfigurator.Domain.Entities;

namespace RetroConfigurator.Infrastructure.Persistence;

public class RetroConfiguratorDbContext : DbContext
{
    public RetroConfiguratorDbContext(DbContextOptions<RetroConfiguratorDbContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);

            entity.Property(o => o.CustomerEmail)
                .IsRequired()
                .HasMaxLength(256);

            entity.Property(o => o.TotalPrice)
                .HasPrecision(18, 2);

            entity.Property(o => o.Status)
                .IsRequired();

            entity.Property(o => o.CreatedAt)
                .IsRequired();

            // Configure ConsoleConfig as owned entity (Value Object)
            entity.OwnsOne(o => o.Configuration, config =>
            {
                config.Property(c => c.ConsoleType).IsRequired();
                config.Property(c => c.NumberOfControllers).IsRequired();
                config.Property(c => c.HDMISupport).IsRequired();
                config.Property(c => c.CustomColor).IsRequired();
                config.Property(c => c.ColorHex).HasMaxLength(7);
            });

            entity.HasIndex(o => o.CustomerEmail);
            entity.HasIndex(o => o.CreatedAt);
        });
    }
}
