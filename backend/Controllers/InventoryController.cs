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

    // Bake Sale
    // GET: api/Inventory/getAllBaking
    [HttpGet("getAllBaking")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        return Ok(await _inventoryService.GetAllProductsAsync());
    }

    // GET: api/Inventory/getBakingById/{id}
    [HttpGet("getBakingById/{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _inventoryService.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    // PUT: api/Inventory/updateBakingById/{id}
    [HttpPut("updateBakingById/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
    {
        var updated = await _inventoryService.UpdateProductAsync(id, productDto);
        return updated ? NoContent() : NotFound($"Product with ID {id} not found.");
    }

    // PUT: api/Inventory/updateBakingCurrentQuantity/{id}
    [HttpPut("updateBakingCurrentQuantity/{id}")]
    public async Task<IActionResult> UpdateCurrentQuantity(int id)
    {
        var updated = await _inventoryService.UpdateCurrentQuantityAsync(id);
        return updated
            ? Ok(new { message = "Quantity updated successfully." })
            : NotFound($"Product with ID {id} not found.");
    }

    // Second-hand items sale
    
    // POST : api/Inventory/addSecondHandItem
    [HttpPost("addSecondHandItem")]
    public async Task<ActionResult<SecondHandItem>> AddSecondHandItem(SecondHandItem secondHandItem)
    {
        var createdItem = await _inventoryService.AddSecondHandItemAsync(secondHandItem);
        return CreatedAtAction(nameof(GetSecondHandItemById), new { id = createdItem.Id }, createdItem);
    }

    
    // GET : api/Inventory/getAllSecondHandItems
    [HttpGet("getAllSecondHandItems")]
    public async Task<ActionResult<IEnumerable<SecondHandItemDto>>> GetAllSecondHandItems()
    {
        return Ok(await _inventoryService.GetAllSecondHandItemsAsync());
    }

    // GET: api/Inventory/getSecondHandItemById/{id}
    [HttpGet("getSecondHandItemById/{id}")]
    public async Task<ActionResult<SecondHandItem>> GetSecondHandItemById(int id)
    {
        var secondHandItem = await _inventoryService.GetSecondHandItemByIdAsync(id);
        if (secondHandItem == null) return NotFound();
        return Ok(secondHandItem);
    }

    // PUT: api/Inventory/updateSecondHandItemById/{id}
    [HttpPut("updateSecondHandItemById/{id}")]
    public async Task<IActionResult> UpdateSecondHandItemById(int id, SecondHandItemDto secondHandItemDto)
    {
        var updated = await _inventoryService.UpdateSecondHandItemAsync(id, secondHandItemDto);
        return updated ? NoContent() : NotFound($"Second-hand item with ID {id} not found.");
    }

    // PUT: api/Inventory/updateSecondHandItemCurrentQuantity/{id}
    [HttpPut("updateSecondHandItemCurrentQuantity/{id}")]
    public async Task<IActionResult> UpdateSecondHandItemCurrentQuantity(int id)
    {
        var updated = await _inventoryService.UpdateCurrentQuantitySecondHandItemAsync(id);
        return updated
            ? Ok(new { message = "Quantity updated successfully." })
            : NotFound($"Second-hand item with ID {id} not found.");
    }
    
}
