using BakeSale.API.Models;
using BakeSale.API.Repository;

namespace BakeSale.API.Services;

public class CheckoutService
{
    private readonly OrderRepository _orderRepository;
    private readonly PaymentService _paymentService;

    public CheckoutService(OrderRepository orderRepository, PaymentService paymentService)
    {
        _orderRepository = orderRepository;
        _paymentService = paymentService;
    }

    public async Task<(bool success, decimal change, string message)> ProcessCheckoutAsync(decimal totalAmount,
        decimal paidAmount)
    {
        if (paidAmount < totalAmount)
            return (false, 0, "Insufficient funds!");

        decimal change = paidAmount - totalAmount;

        try
        {
            await _paymentService.AddPaymentAsync(paidAmount, change);

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount
            };

            await _orderRepository.AddOrderAsync(order);

            return (true, change, "Payment completed successfully!");
        }
        catch (Exception ex)
        {
            return (false, 0, $"Error processing order: {ex.Message}");
        }
    }
}