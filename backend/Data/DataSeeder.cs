using BakeSale.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeSale.API.Data;

public class DataSeeder
{
    private readonly BakeSaleContext _context;

    public DataSeeder(BakeSaleContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        var getExistingProduct = await _context.Products
            .Select(p => p.Title)
            .ToListAsync();

        var products = new List<Product>
        {
            new Product { Cost = 0.65m, Title = "Brownie", StartingQuantity = 48, CurrentQuantity = 48 , ProductType = "Baking"},
            new Product { Cost = 1.00m, Title = "Muffin", StartingQuantity = 36, CurrentQuantity = 36, ProductType = "Baking" },
            new Product { Cost = 1.35m, Title = "Cake Pop", StartingQuantity = 24, CurrentQuantity = 24, ProductType = "Baking" },
            new Product { Cost = 1.50m, Title = "Apple tart", StartingQuantity = 60, CurrentQuantity = 60, ProductType = "Baking" },
            new Product { Cost = 1.50m, Title = "Water", StartingQuantity = 30, CurrentQuantity = 30, ProductType = "Baking" }
        };

        var addMissingProducts = products
            .Where(p => !getExistingProduct.Contains(p.Title))
            .ToList();

        if (addMissingProducts.Any())
        {
            await _context.Products.AddRangeAsync(addMissingProducts);
            await _context.SaveChangesAsync();   
        }
    }
}