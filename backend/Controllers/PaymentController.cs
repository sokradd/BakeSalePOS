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

        public PaymentController(PaymentService paymentService, PaymentRepository paymentRepository)
        {
            _paymentService = paymentService;
            _paymentRepository = paymentRepository;
        }

        // POST : api/processPayment
        [HttpPost("processPayment")]
        public async Task<ActionResult<PaymentDto>> ProcessPayment([FromBody] PaymentDto paymentDto)
        {
            try
            {
                var payment = await _paymentService.ProcessPaymentAsync(paymentDto.OrderId, paymentDto.CashPaid,
                    paymentDto.CashPaid - paymentDto.ChangeReturned + paymentDto.ChangeReturned);
                var resultDto = new PaymentDto
                {
                    Id = payment.Id,
                    OrderId = payment.OrderId,
                    CashPaid = payment.CashPaid,
                    ChangeReturned = payment.ChangeReturned,
                    PaymentDate = payment.PaymentDate
                };
                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //GET : api/getAllPayments
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