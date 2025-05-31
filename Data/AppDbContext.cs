using Microsoft.EntityFrameworkCore;
using ConfectioneryManagementApp.Models;
using ConfectioneryManagementApp.Models.Entities;

namespace ConfectioneryManagementApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}
        
        public DbSet<IngredientEntity> Ingredients { get; set; }
        public DbSet<PastryEntity> Pastries { get; set; }
        public DbSet<CakeEntity> Cakes { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CakeIngredientEntity>()
                .HasKey(ci => new { ci.CakeId, ci.IngredientId });

            modelBuilder.Entity<PastryIngredientEntity>()
                .HasKey(pi => new { pi.PastryId, pi.IngredientId });
            
            modelBuilder.Entity<OrderEntity>()
                .HasMany(o => o.Cakes)
                .WithMany()
                .UsingEntity(j => j.ToTable("OrderPastries"));

            modelBuilder.Entity<OrderEntity>()
                .HasMany(o => o.OrderedCakes)
                .WithMany()
                .UsingEntity(j => j.ToTable("OrderCakes"));
        }
    }
}