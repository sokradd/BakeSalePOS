using BakeSale.API.DTOs;
using BakeSale.API.Models;
using BakeSale.API.Repositories;

namespace BakeSale.API.Services;

public class InventoryService
{
    private readonly ProductRepository _productRepository;
    

    public InventoryService(ProductRepository productRepository )
    {
        _productRepository = productRepository;
    }

    // Bake Sale
    
    public async Task<Product> AddProductAsync(Product product)
    {
        return await _productRepository.AddProductAsync(product);
    }
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return products.Select(p => new ProductDto { Title = p.Title, Cost = p.Cost , ProductType = p.ProductType }).ToList();
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

    // // Second-hand items sale
    // public async Task<SecondHandItem> AddSecondHandItemAsync(SecondHandItem secondHandItem)
    // {
    //     return await _secondHandItemRepository.AddSecondHandItemAsync(secondHandItem);
    // }
    //
    // public async Task<IEnumerable<SecondHandItemDto>> GetAllSecondHandItemsAsync()
    // {
    //     var secondHandItems = await _secondHandItemRepository.GetAllSecondHandItemsAsync();
    //     return secondHandItems.Select(p => new SecondHandItemDto() { Title = p.Title, Cost = p.Cost }).ToList();
    // }
    //
    // public async Task<SecondHandItem?> GetSecondHandItemByIdAsync(int id)
    // {
    //     return await _secondHandItemRepository.GetSecondHandItemByIdAsync(id);
    // }
    //
    // public async Task<bool> UpdateSecondHandItemAsync(int id, SecondHandItemDto secondHandItemDto)
    // {
    //     var secondHandItem = await _secondHandItemRepository.GetSecondHandItemByIdAsync(id);
    //     if (secondHandItem == null) return false;
    //
    //     secondHandItem.Title = secondHandItemDto.Title;
    //     secondHandItem.Cost = secondHandItemDto.Cost;
    //
    //     await _secondHandItemRepository.UpdateSecondHandItemAsync(secondHandItem);
    //     return true;
    // }
    //
    // public async Task<bool> DecreaseCurrentQuantitySecondHandItemAsync(int id)
    // {
    //     var secondHandItem = await _secondHandItemRepository.GetSecondHandItemByIdAsync(id);
    //     if (secondHandItem == null || secondHandItem.CurrentQuantity == 0) return false;
    //
    //     secondHandItem.CurrentQuantity -= 1;
    //     await _secondHandItemRepository.UpdateSecondHandItemAsync(secondHandItem);
    //     return true;
    // }
    // public async Task<bool> IncreaseCurrentQuantitySecondHandItemAsync(int id)
    // {
    //     var secondHandItem = await _secondHandItemRepository.GetSecondHandItemByIdAsync(id);
    //     if (secondHandItem == null || secondHandItem.CurrentQuantity == 0) return false;
    //
    //     secondHandItem.CurrentQuantity += 1;
    //     await _secondHandItemRepository.UpdateSecondHandItemAsync(secondHandItem);
    //     return true;
    // }
    //
}