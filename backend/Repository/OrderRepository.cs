using BakeSale.API.Data;
using BakeSale.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeSale.API.Repositories;

public class OrderRepository
{
    private readonly BakeSaleContext _context;

    public OrderRepository(BakeSaleContext context)
    {
        _context = context;
    }

    public async Task<Order> AddOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }
    
    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.OrderLines)
            .Include(o => o.Salesperson) 
            .FirstOrDefaultAsync(o => o.Id == id);
    }
    
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderLines)
            .Include(o => o.Salesperson)
            .ToListAsync();
    }
    
    public async Task UpdateOrderAsync(Order order)
    {
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteOrderAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}