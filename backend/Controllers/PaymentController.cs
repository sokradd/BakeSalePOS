using BakeSale.API.DTOs;
using BakeSale.API.Models;
using BakeSale.API.Repositories;
using BakeSale.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BakeSale.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private readonly PaymentRepository _paymentRepository;
        private readonly OrderService _orderService;

        public PaymentController(PaymentService paymentService, PaymentRepository paymentRepository,
            OrderService orderService)
        {
            _paymentService = paymentService;
            _paymentRepository = paymentRepository;
            _orderService = orderService;
        }

        
        //POST : api/payment/processPayment
        [HttpPost("processPayment")]
        public async Task<ActionResult<Payment>> ProcessPayment(Payment payment)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(payment.OrderId);
                if (order == null)
                {
                    return NotFound("Заказ не найден.");
                }
                
                var addPayment =
                    await _paymentService.ProcessPaymentAsync(payment.OrderId, payment.CashPaid, order.TotalAmount);
                var result = new Payment
                {
                    Id = addPayment.Id,
                    OrderId = addPayment.OrderId,
                    CashPaid = addPayment.CashPaid,
                    ChangeReturned = addPayment.ChangeReturned,
                    PaymentDate = addPayment.PaymentDate
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET : api/payment/getAllPayments
        [HttpGet("getAllPayments")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPayments();
            var dtoList = payments.Select(p => new PaymentDto
            {
                Id = p.Id,
                OrderId = p.OrderId,
                CashPaid = p.CashPaid,
                ChangeReturned = p.ChangeReturned,
                PaymentDate = p.PaymentDate
            });
            return Ok(dtoList);
        }

        // GET: api/payment/getPaymentById/{id}
        [HttpGet("getPaymentById/{id}")]
        public async Task<ActionResult<Payment>> GetPaymentById(int id)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }
    }
}