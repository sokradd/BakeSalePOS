using BakeSale.API.Services;
using Microsoft.AspNetCore.Mvc;
using BakeSale.API.Models;

namespace BakeSale.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly PaymentService _paymentService;
    private readonly CheckoutService _checkoutService;

    public PaymentController(PaymentService paymentService, CheckoutService checkoutService)
    {
        _paymentService = paymentService;
        _checkoutService = checkoutService;
    }

    // POST : api/payment/checkout
    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout(decimal totalAmount, decimal paidAmount)
    {
        var (success, change, message) = await _checkoutService.ProcessCheckoutAsync(totalAmount, paidAmount);

        if (success)
            return Ok(new { message, change });

        return BadRequest(new { message });
    }
    
    // GET : api/payment/getAllPayments
    [HttpGet("getAllPayments")]
    public async Task<ActionResult<IEnumerable<Payment>>> GetAllPayments()
    {
        var payments = await _paymentService.GetAllPaymentsAsync();
        return Ok(payments);
    }
}