using BakeSale.API.DTOs;
using BakeSale.API.Models;

namespace BakeSale.API.Services;

public interface IInventoryService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<bool> UpdateProductAsync(int id, ProductDto productDto);
    Task<bool> UpdateCurrentQuantityAsync(int id);
}