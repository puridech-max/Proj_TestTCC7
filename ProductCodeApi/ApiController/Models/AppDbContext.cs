using Microsoft.EntityFrameworkCore;

namespace ProductCodeApi.ApiController.Models;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(e =>
        {
            e.Property(p => p.PluCode).IsRequired();
            e.HasIndex(p => p.PluCode).IsUnique();
        });
    }
}
