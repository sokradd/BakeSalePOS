using BakeSale.API.Data;
using BakeSale.API.DTOs;
using BakeSale.API.Models;
using BakeSale.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BakeSale.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public InventoryController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    //Get : api/Inventory
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        var products = await _productRepository.GetAllProductsAsync();
        var productsDtos = products.Select(p => new ProductDto
        {
            Title = p.Title,
            Cost = p.Cost
        }).ToList();

        return Ok(productsDtos);
    }


    //Get : api/Inventory/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    //Put : api/Inventory/{id}/
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null) return NotFound($"Product with ID {id} not found.");

        product.Title = productDto.Title;
        product.Cost = productDto.Cost;

        await _productRepository.UpdateProductAsync(product);
        return NoContent();
    }

    //Put : api/Inventory/{id}/UpdateCurrentQuantity
    [HttpPut("{id}/UpdateCurrentQuantity")]
    public async Task<IActionResult> UpdateCurrentQuantity(int id, [FromBody] int newQuantity)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound($"Product with ID {id} not found.");
        }

        if (product.CurrentQuantity <= 0)
        {
            return BadRequest("The product is out of stock.");
        }

        product.CurrentQuantity -= 1;

        await _productRepository.UpdateProductAsync(product);
        return Ok(new { message = "Quantity updated successfully.", newQuantity = product.CurrentQuantity });
    }
}