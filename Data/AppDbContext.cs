using Microsoft.EntityFrameworkCore;
using ConfectioneryManagementApp.Models;

namespace ConfectioneryManagementApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        // public DbSet<Client> Clients { get; set; }
        // public DbSet<Order> Orders { get; set; }
        // public DbSet<Cake> Cakes { get; set; }
        // public DbSet<Ingredient> Ingredients { get; set; }
        // public DbSet<OrderCake> OrderCakes { get; set; }
        // public DbSet<CakeIngredient> CakeIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}