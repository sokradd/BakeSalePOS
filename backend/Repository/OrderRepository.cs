using BakeSale.API.Data;
using BakeSale.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeSale.API.Repository;

public class OrderRepository
{
    private readonly BakeSaleContext _context;

    public OrderRepository(BakeSaleContext context)
    {
        _context = context;
    }

    public async Task AddOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.AsNoTracking().ToListAsync();
    }
}