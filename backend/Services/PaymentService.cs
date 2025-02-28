using BakeSale.API.Models;
using BakeSale.API.Repository;

namespace BakeSale.API.Services;

public class PaymentService
{
    private readonly PaymentRepository _paymentRepository;

    public PaymentService(PaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task AddPaymentAsync(decimal amountPaid, decimal changeGiven)
    {
        var payment = new Payment
        {
            AmountPaid = amountPaid,
            ChangeGiven = changeGiven,
            PaymentDate = DateTime.UtcNow
        };

        await _paymentRepository.AddPaymentAsync(payment);
    }

    public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
    {
        return await _paymentRepository.GetAllPaymentsAsync();
    }
}