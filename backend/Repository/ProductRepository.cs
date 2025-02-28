using BakeSale.API.Data;
using BakeSale.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeSale.API.Repositories;

public class ProductRepository 
{
    private readonly BakeSaleContext _context;

    public ProductRepository(BakeSaleContext context)
    {
        _context = context;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    
}