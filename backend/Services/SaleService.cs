using BakeSale.API.Models;
using BakeSale.API.Repositories;
using BakeSale.API.Repository;

namespace BakeSale.API.Services;

public class SaleService
{
    private readonly ProductRepository _productRepository;
    private readonly SecondHandItemRepository _secondHandItemRepository;
    private readonly OrderRepository _orderRepository;

    public SaleService(ProductRepository productRepository,
        SecondHandItemRepository secondHandItemRepository,
        OrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _secondHandItemRepository = secondHandItemRepository;
        _orderRepository = orderRepository;
    }
    
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllOrdersAsync();
    }

    public async Task<bool> ProcessSaleAsync(int id, int quantity, bool isSecondHand)
    {
        if (isSecondHand)
        {
            var secondHandItem = await _secondHandItemRepository.GetSecondHandItemByIdAsync(id);
            if (secondHandItem == null || secondHandItem.CurrentQuantity < quantity) return false;
            secondHandItem.CurrentQuantity -= quantity;
            await _secondHandItemRepository.UpdateSecondHandItemAsync(secondHandItem);
        }
        else
        {
            var bakingItem = await _productRepository.GetProductByIdAsync(id);
            if (bakingItem == null || bakingItem.CurrentQuantity < quantity) return false;
            bakingItem.CurrentQuantity -= quantity;
            await _productRepository.UpdateProductAsync(bakingItem);
        }

        return true;
    }
}