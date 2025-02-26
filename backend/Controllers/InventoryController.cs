using BakeSale.API.Data;
using BakeSale.API.DTOs;
using BakeSale.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BakeSale.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly BakeSaleContext _bakeSaleContext;

    public InventoryController(BakeSaleContext bakeSaleContext)
    {
        _bakeSaleContext = bakeSaleContext;
    }

    //Get : api/Inventory
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        var products = await _bakeSaleContext.Products
            .Select(p => new ProductDto
            {
                Title = p.Title,
                Cost = p.Cost
            })
            .ToListAsync();

        return Ok(products);
    }


    //Get : api/Inventory/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("ID suppose to be more than 0.");
        }

        var product = await _bakeSaleContext.Products.FindAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    //Put : api/Inventory/{id}/
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
    {
        var product = await _bakeSaleContext.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound($"Product with ID {id} not found.");
        }

        product.Title = productDto.Title;
        product.Cost = productDto.Cost;

        await _bakeSaleContext.SaveChangesAsync();
        return NoContent();
    }

    //Put : api/Inventory/{id}/UpdateCurrentQuantity
    [HttpPut("{id}/UpdateCurrentQuantity")]
    public async Task<IActionResult> UpdateCurrentQuantity(int id)
    {
        var product = await _bakeSaleContext.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound($"Product with ID {id} not found.");
        }

        if (product.CurrentQuantity <= 0)
        {
            return BadRequest("The product is out of stock.");
        }

        product.CurrentQuantity -= 1;

        await _bakeSaleContext.SaveChangesAsync();
        return Ok(new { message = "Quantity updated successfully.", newQuantity = product.CurrentQuantity });
    }
}