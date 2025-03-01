using BakeSale.API.DTOs;
using BakeSale.API.Models;
using BakeSale.API.Repositories;

namespace BakeSale.API.Services;

public class InventoryService
{
    private readonly ProductRepository _productRepository;


    public InventoryService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        return await _productRepository.AddProductAsync(product);
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return products.Select(p => new ProductDto
        {
            Id = p.Id,Title = p.Title, Cost = p.Cost, ProductType = p.ProductType, StartingQuantity = p.StartingQuantity,
            CurrentQuantity = p.CurrentQuantity
        }).ToList();
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
        product.ProductType = productDto.ProductType;

        await _productRepository.UpdateProductAsync(product);
        return true;
    }

    public async Task<bool> DecreaseCurrentQuantityAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null || product.CurrentQuantity == 0) return false;

        product.CurrentQuantity -= 1;
        await _productRepository.UpdateProductAsync(product);
        return true;
    }

    public async Task<bool> IncreaseCurrentQuantityAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null || product.CurrentQuantity == 0) return false;

        product.CurrentQuantity += 1;
        await _productRepository.UpdateProductAsync(product);
        return true;
    }
}