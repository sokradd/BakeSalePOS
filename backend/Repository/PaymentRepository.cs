using BakeSale.API.Data;
using BakeSale.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeSale.API.Repository;

public class PaymentRepository
{
    private readonly BakeSaleContext _context;

    public PaymentRepository(BakeSaleContext context)
    {
        _context = context;
    }

    public async Task AddPaymentAsync(Payment payment)
    {
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
    {
        return await _context.Payments.AsNoTracking().ToListAsync();
    }
}