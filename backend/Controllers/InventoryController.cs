using BakeSale.API.Data;
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
    
    //Get : api/Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        return await _bakeSaleContext.Products.ToListAsync();
    }
    
    //Get : api/Products/{id}
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
    
}