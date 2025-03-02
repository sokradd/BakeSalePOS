using BakeSale.API.Models;
using BakeSale.API.Repositories;

namespace BakeSale.API.Services;

public class PaymentService
{
    private readonly PaymentRepository _paymentRepository;

    public PaymentService(PaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<IEnumerable<Payment>> GetAllPayments()
    {
        return await _paymentRepository.GetAllPaymentsAsync();
    }
    public decimal CalculateChange(decimal cashPaid, decimal totalAmount)
    {
        return cashPaid - totalAmount;
    }
    
    public async Task<Payment> ProcessPaymentAsync(int orderId, decimal cashPaid, decimal totalAmount)
    {
        var change = CalculateChange(cashPaid, totalAmount);

        var payment = new Payment
        {
            OrderId = orderId,
            CashPaid = cashPaid,
            ChangeReturned = change,
            PaymentDate = DateTime.UtcNow
        };

        await _paymentRepository.AddPaymentAsync(payment);
        return payment;
    }
}