using BakeSale.API.DTOs;
using BakeSale.API.Models;
using BakeSale.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BakeSale.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly InventoryService _inventoryService;

    public InventoryController(InventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }
    
    // POST : api/Inventory/addProduct
    [HttpPost("addProduct")]
    public async Task<ActionResult<Product>> AddProduct(Product product)
    {
        var createdItem = await _inventoryService.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = createdItem.Id }, createdItem);
    }
    // GET: api/Inventory/getAllProducts
    [HttpGet("getAllProducts")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        return Ok(await _inventoryService.GetAllProductsAsync());
    }

    // GET: api/Inventory/getProductById/{id}
    [HttpGet("getProductById/{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _inventoryService.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    // PUT: api/Inventory/updateProductById/{id}
    [HttpPut("updateProductsById/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
    {
        var updated = await _inventoryService.UpdateProductAsync(id, productDto);
        return updated ? NoContent() : NotFound($"Product with ID {id} not found.");
    }

    // PUT: api/Inventory/decreaseProductCurrentQuantity/{id}
    [HttpPut("decreaseProductCurrentQuantity/{id}")]
    public async Task<IActionResult> DecreaseCurrentQuantity(int id)
    {
        var updated = await _inventoryService.DecreaseCurrentQuantityAsync(id);
        return updated
            ? Ok(new { message = "Quantity decreased successfully." })
            : NotFound($"Product with ID {id} not found.");
    }
    
    // PUT: api/Inventory/increaseProductCurrentQuantity/{id}
    [HttpPut("increaseProductCurrentQuantity/{id}")]
    public async Task<IActionResult> IncreaseCurrentQuantity(int id)
    {
        var updated = await _inventoryService.IncreaseCurrentQuantityAsync(id);
        return updated
            ? Ok(new { message = "Quantity increased successfully." })
            : NotFound($"Product with ID {id} not found.");
    }
}
