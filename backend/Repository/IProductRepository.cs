using BakeSale.API.Models;

namespace BakeSale.API.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task UpdateProductAsync(Product product);
    Task<bool> ProductExistsAsync(int id);
}