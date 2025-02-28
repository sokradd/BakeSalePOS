using BakeSale.API.Models;
using BakeSale.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BakeSale.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly SaleService _saleService;

    public SalesController(SaleService saleService)
    {
        _saleService = saleService;
    }
    
    // GET : api/Sales/getAllOrders
    [HttpGet("getAllOrders")]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        var orders = await _saleService.GetAllOrdersAsync();
        return Ok(orders);
    }
    
    // POST : api/Sales/processSale
    [HttpPost("processSale")]
    public async Task<IActionResult> ProcessSale(int id, int quantity, bool isSecondHand)
    {
        var success = await _saleService.ProcessSaleAsync(id, quantity, isSecondHand);
        return success ? Ok("Sale processed successfully") : BadRequest("Not enough stock");
    }
}