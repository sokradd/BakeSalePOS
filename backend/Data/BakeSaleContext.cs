using BakeSale.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeSale.API.Data;

public class BakeSaleContext : DbContext
{
    public BakeSaleContext(DbContextOptions<BakeSaleContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Salesperson> Salespersons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderLine>()
            .HasOne(ol => ol.Order)
            .WithMany(o => o.OrderLines)
            .HasForeignKey(ol => ol.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderLine>()
            .HasOne(ol => ol.Product)
            .WithMany(p => p.OrderLines)
            .HasForeignKey(ol => ol.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Salesperson)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.SalespersonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}