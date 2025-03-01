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

        // POST : api/order/createOrder
        [HttpPost("createOrder")]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] OrderDto orderDto)
        {
            try
            {
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    Status = orderDto.Status,
                    SalespersonId = orderDto.SalespersonId,
                    OrderLines = orderDto.OrderLines.Select(olDto => new OrderLine
                    {
                        ProductId = olDto.ProductId,
                        Quantity = olDto.Quantity
                    }).ToList()
                };

                var createdOrder = await _orderService.CreateOrderAsync(order);

                var createdOrderDto = new OrderDto
                {
                    Id = createdOrder.Id,
                    OrderDate = createdOrder.OrderDate,
                    TotalAmount = createdOrder.TotalAmount,
                    Status = createdOrder.Status,
                    SalespersonId = createdOrder.SalespersonId,
                    Salesperson = createdOrder.Salesperson != null
                        ? new SalespersonDto
                        {
                            Id = createdOrder.Salesperson.Id,
                            Name = createdOrder.Salesperson.Name
                        }
                        : null,
                    OrderLines = createdOrder.OrderLines.Select(ol => new OrderLineDto
                    {
                        Id = ol.Id,
                        OrderId = ol.OrderId,
                        ProductId = ol.ProductId,
                        Quantity = ol.Quantity,
                        Product = ol.Product != null
                            ? new ProductDto
                            {
                                Id = ol.Product.Id,
                                Title = ol.Product.Title,
                                Cost = ol.Product.Cost,
                                ProductType = ol.Product.ProductType,
                                StartingQuantity = ol.Product.StartingQuantity,
                                CurrentQuantity = ol.Product.CurrentQuantity
                            }
                            : null
                    }).ToList()
                };

                return CreatedAtAction(nameof(GetOrderById), new { id = createdOrderDto.Id }, createdOrderDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET : api/order/getOrderById/{id}
        [HttpGet("getOrderById/{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                var orderDto = new OrderDto
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    Status = order.Status,
                    SalespersonId = order.SalespersonId,
                    Salesperson = order.Salesperson != null
                        ? new SalespersonDto
                        {
                            Id = order.Salesperson.Id,
                            Name = order.Salesperson.Name
                        }
                        : null,
                    OrderLines = order.OrderLines.Select(ol => new OrderLineDto
                    {
                        Id = ol.Id,
                        OrderId = ol.OrderId,
                        ProductId = ol.ProductId,
                        Quantity = ol.Quantity,
                        Product = ol.Product != null
                            ? new ProductDto
                            {
                                Id = ol.Product.Id,
                                Title = ol.Product.Title,
                                Cost = ol.Product.Cost,
                                ProductType = ol.Product.ProductType,
                                StartingQuantity = ol.Product.StartingQuantity,
                                CurrentQuantity = ol.Product.CurrentQuantity
                            }
                            : null
                    }).ToList()
                };

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT : api/order/checkoutOrder/{orderId}
        [HttpPut("checkoutOrder/{orderId}")]
        public async Task<ActionResult<PaymentDto>> CheckoutOrder(int orderId, [FromBody] decimal cashAmount)
        {
            try
            {
                var change = await _orderService.ProcessPaymentAsync(orderId, cashAmount);
                var paymentDto = new PaymentDto
                {
                    OrderId = orderId,
                    ChangeReturned = change,
                    PaymentDate = DateTime.Now
                };
                return Ok(paymentDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT : api/order/resetOrder/{orderId}
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
    }
}