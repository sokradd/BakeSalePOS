using BakeSale.API.DTOs;
using BakeSale.API.Models;
using BakeSale.API.Repositories;

namespace BakeSale.API.Services;

public class InventoryService : IInventoryService
{
    private readonly IProductRepository _productRepository;

    public InventoryService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return products.Select(p => new ProductDto { Title = p.Title, Cost = p.Cost }).ToList();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetProductByIdAsync(id);
    }

    public async Task<bool> UpdateProductAsync(int id, ProductDto productDto)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null) return false;

        product.Title = productDto.Title;
        product.Cost = productDto.Cost;

        await _productRepository.UpdateProductAsync(product);
        return true;
    }

    public async Task<bool> UpdateCurrentQuantityAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null || product.CurrentQuantity == 0) return false;

        product.CurrentQuantity -= 1;
        await _productRepository.UpdateProductAsync(product);
        return true;
    }
}