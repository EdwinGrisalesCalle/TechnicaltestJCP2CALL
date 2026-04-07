using Microsoft.EntityFrameworkCore;
using module2.Entities;
using System.Reflection.Emit;

namespace module2.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQL Server LocalDB (for development)
            optionsBuilder.UseSqlite("Data Source=store.db");

            // OR SQLite (alternative if no SQL Server)
            // optionsBuilder.UseSqlite("Data Source=store.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationship
            modelBuilder.Entity<Order>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
