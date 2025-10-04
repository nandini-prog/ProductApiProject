using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    // DbContext = EF Core’s “gateway” to the database
    public class ApplicationDbContext : DbContext
    {
        // constructor: gets options (connection string, provider)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet<T> represents a table in DB
        public DbSet<Product> Products => Set<Product>();

        // Configure entity-to-table mapping & constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: configure Product table columns
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.ProductName)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(p => p.CreatedBy)
                      .HasMaxLength(100);
            });
        }
    }
}
