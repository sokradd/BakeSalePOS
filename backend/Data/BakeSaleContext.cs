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
        
    }
}