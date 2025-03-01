using BakeSale.API.Models;
using BakeSale.API.Repositories;

namespace BakeSale.API.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly PaymentService _paymentService;

    public OrderService(OrderRepository orderRepository, PaymentService paymentService)
    {
        _orderRepository = orderRepository;
        _paymentService = paymentService;
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        order.OrderDate = DateTime.Now;
        order.TotalAmount = order.OrderLines.Sum(line => line.Quantity * line.Product.Cost);
        return await _orderRepository.AddOrderAsync(order);
    }

    public async Task<decimal> ProcessPaymentAsync(int orderId, decimal cashAmount)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        if (order == null)
        {
            throw new Exception("Order no found.");
        }

        if (cashAmount < order.TotalAmount)
        {
            throw new Exception("Insufficient funds");
        }

        var payment = await _paymentService.ProcessPaymentAsync(orderId, cashAmount, order.TotalAmount);
        order.Status = Status.Paid.ToString();
        await _orderRepository.UpdateOrderAsync(order);

        return payment.ChangeReturned;
    }

    public async Task ResetOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        if (order == null)
        {
            throw new Exception("Order not found.");
        }

        foreach (var line in order.OrderLines)
        {
            line.Product.CurrentQuantity += line.Quantity;
        }

        order.Status = Status.Canceled.ToString();
        await _orderRepository.UpdateOrderAsync(order);
        
    }
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllOrdersAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersBySalespersonIdAsync(int salespersonId)
    {
        return await _orderRepository.GetOrdersBySalespersonIdAsync(salespersonId);
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);
        if (order == null)
            throw new Exception("Order not found.");
        return order;
    }
}