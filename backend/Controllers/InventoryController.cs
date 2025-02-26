using BakeSale.API.DTOs;
using BakeSale.API.Models;
using BakeSale.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BakeSale.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    // GET: api/Inventory
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        return Ok(await _inventoryService.GetAllProductsAsync());
    }

    // GET: api/Inventory/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _inventoryService.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    // PUT: api/Inventory/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
    {
        var updated = await _inventoryService.UpdateProductAsync(id, productDto);
        return updated ? NoContent() : NotFound($"Product with ID {id} not found.");
    }

    // PUT: api/Inventory/{id}/UpdateCurrentQuantity
    [HttpPut("{id}/UpdateCurrentQuantity")]
    public async Task<IActionResult> UpdateCurrentQuantity(int id)
    {
        var updated = await _inventoryService.UpdateCurrentQuantityAsync(id);
        return updated ? Ok(new { message = "Quantity updated successfully." }) 
                       : NotFound($"Product with ID {id} not found.");
    }
}
