using Microsoft.EntityFrameworkCore;
using ConfectioneryManagementApp.Models;
using ConfectioneryManagementApp.Models.Entities;

namespace ConfectioneryManagementApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}
        
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<CakeEntity> Cakes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}