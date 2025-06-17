using Microsoft.EntityFrameworkCore;
using VendingMachineApi.Models;

namespace VendingMachineApi
{
    public class AppDbContext : DbContext
    {
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Coin> Coins => Set<Coin>();
        public DbSet<Order> Orders => Set<Order>();
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(o =>
            {
                o.Property(o => o.Details).HasColumnType("jsonb");
            });
            modelBuilder.Entity<Brand>(b =>
            {
                b.HasIndex(b => b.Name).IsUnique();
            });
            modelBuilder.Entity<Product>(p =>
            {
                p.HasIndex(p => p.Name).IsUnique();
            });
        }
    }
}
