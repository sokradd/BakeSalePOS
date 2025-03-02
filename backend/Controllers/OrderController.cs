using BakeSale.API.DTOs;
using BakeSale.API.Models;
using BakeSale.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BakeSale.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
    
        // POST: api/order/createOrder
        [HttpPost("createOrder")]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            try
            {
                var createdOrder = await _orderService.CreateOrderAsync(order);
                return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
        // GET: api/order/getOrderById/{id}
        [HttpGet("getOrderById/{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    
        // GET: api/order/getAllOrders
        [HttpGet("getAllOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
        // PUT: api/order/checkoutOrder/{orderId}
        [HttpPut("checkoutOrder/{orderId}")]
        public async Task<ActionResult<PaymentDto>> CheckoutOrder(int orderId, [FromBody] decimal cashAmount)
        {
            try
            {
                var change = await _orderService.ProcessPaymentAsync(orderId, cashAmount);
                var paymentDto = new PaymentDto
                {
                    OrderId = orderId,
                    CashPaid = cashAmount,
                    ChangeReturned = change,
                    PaymentDate = DateTime.UtcNow
                };
                return Ok(paymentDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
        // PUT: api/order/resetOrder/{orderId}
        [HttpPut("resetOrder/{orderId}")]
        public async Task<IActionResult> ResetOrder(int orderId)
        {
            try
            {
                await _orderService.ResetOrderAsync(orderId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
        // PUT: api/order/updateOrder/{orderId}
        [HttpPut("updateOrder/{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] Order updatedOrder)
        {
            try
            {
                var result = await _orderService.UpdateOrderAsync(orderId, updatedOrder);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
