using BakeSale.API.Models;
using BakeSale.API.Repositories;

namespace BakeSale.API.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly PaymentService _paymentService;
        private readonly ProductRepository _productRepository;

        public OrderService(OrderRepository orderRepository, PaymentService paymentService, ProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _paymentService = paymentService;
            _productRepository = productRepository;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            return await _orderRepository.AddOrderAsync(order);
        }

        public async Task<decimal> ProcessPaymentAsync(int orderId, decimal cashPaid)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
                throw new Exception("Order not found.");
            
            foreach (var line in order.OrderLines)
            {
                var product = await _productRepository.GetProductByIdAsync(line.ProductId);
                if (product == null)
                    throw new Exception($"Product with ID {line.ProductId} not found.");
                if (product.CurrentQuantity < line.Quantity)
                    throw new Exception($"Insufficient stock for product {product.Title}.");
            }
            
            if (cashPaid < order.TotalAmount)
                throw new Exception("Insufficient funds.");
            
            var payment = await _paymentService.ProcessPaymentAsync(orderId, cashPaid, order.TotalAmount);
            
            order.Status = Status.Paid.ToString();
            await _orderRepository.UpdateOrderAsync(order);
            
            foreach (var line in order.OrderLines)
            {
                var product = await _productRepository.GetProductByIdAsync(line.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {line.ProductId} not found.");
                }
                product.CurrentQuantity -= line.Quantity;
                if (product.CurrentQuantity < 0)
                    product.CurrentQuantity = 0;
                await _productRepository.UpdateProductAsync(product);
            }

            return payment.ChangeReturned;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<Order> UpdateOrderAsync(int orderId, Order updatedOrder)
        {
            if (orderId != updatedOrder.Id)
            {
                throw new Exception("Order ID in URL does not match ID in body.");
            }

            return await _orderRepository.UpdateOrderAsync(updatedOrder);
        }
        
        public async Task ResetOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
                throw new Exception("Order not found.");
            
            foreach (var line in order.OrderLines)
            {
                var product = await _productRepository.GetProductByIdAsync(line.ProductId);
                if (product != null)
                {
                    product.CurrentQuantity = line.Quantity;
                    await _productRepository.UpdateProductAsync(product);
                }
            }
            
            order.Status = Status.Canceled.ToString();
            await _orderRepository.UpdateOrderAsync(order);
        }
        
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                throw new Exception("Order not found.");
            return order;
        }
    }
}
