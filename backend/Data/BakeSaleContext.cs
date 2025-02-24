using BakeSale.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeSale.API.Data;

public class BakeSaleContext : DbContext
{
   public BakeSaleContext(DbContextOptions<BakeSaleContext> options) : base(options) {}
   
   public DbSet<Order> Orders { get; set; }
   public DbSet<Payment> Payments { get; set; }
   public DbSet<Product> Products { get; set; }
   public DbSet<SecondHandItem> SecondHandItems { get; set; }
   
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<Product>().HasKey(p => p.Id);
      modelBuilder.Entity<Order>().HasKey(o => o.Id);
      modelBuilder.Entity<Payment>().HasKey(p => p.Id);
      modelBuilder.Entity<SecondHandItem>().HasKey(s => s.Id);
   }
}