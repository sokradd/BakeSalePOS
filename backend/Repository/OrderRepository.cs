using BakeSale.API.Data;
using BakeSale.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeSale.API.Repositories
{
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
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderLines)
                .ToListAsync();
        }
        
        public async Task<Order> UpdateOrderAsync(Order updatedOrder)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.OrderLines)
                .FirstOrDefaultAsync(o => o.Id == updatedOrder.Id);

            if (existingOrder == null)
            {
                throw new Exception($"Order with ID {updatedOrder.Id} not found.");
            }

            existingOrder.OrderDate = updatedOrder.OrderDate;
            existingOrder.TotalAmount = updatedOrder.TotalAmount;
            existingOrder.Status = updatedOrder.Status;
            existingOrder.SalespersonId = updatedOrder.SalespersonId;
            existingOrder.OrderLines = updatedOrder.OrderLines;

            foreach (var line in existingOrder.OrderLines)
            {
                line.OrderId = existingOrder.Id;
            }

            await _context.SaveChangesAsync();
            return existingOrder;
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
}
