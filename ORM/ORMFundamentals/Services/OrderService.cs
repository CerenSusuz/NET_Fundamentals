using ORMFundamentals.Entities;
using ORMFundamentals.Repositories;

namespace ORMFundamentals.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _orderRepository.GetAllOrders();
    }

    public async Task<Order> GetOrderById(int id)
    {
        return await _orderRepository.GetOrderById(id);
    }

    public async Task Add(Order order)
    {
        await _orderRepository.AddOrder(order);
    }

    public async Task Update(Order order)
    {
        await _orderRepository.UpdateOrder(order);
    }

    public async Task Delete(int id)
    {
        await _orderRepository.DeleteOrder(id);
    }

    public async Task<IEnumerable<Order>> GetOrdersByMonth(int month)
    {
        return await _orderRepository.GetOrdersByCondition(o => o.CreatedDate.Month == month);
    }

    public async Task<IEnumerable<Order>> GetOrdersByStatus(Status status)
    {
        return await _orderRepository.GetOrdersByCondition(o => o.Status == status);
    }

    public async Task<IEnumerable<Order>> GetOrdersByYear(int year)
    {
        return await _orderRepository.GetOrdersByCondition(o => o.CreatedDate.Year == year);
    }

    public async Task<IEnumerable<Order>> GetOrdersByProduct(int productId)
    {
        return await _orderRepository.GetOrdersByCondition(o => o.ProductId == productId);
    }

    public async Task DeleteOrdersByMonth(int month)
    {
        await _orderRepository.DeleteOrders(o => o.CreatedDate.Month == month);
    }

    public async Task DeleteOrdersByStatus(Status status)
    {
        await _orderRepository.DeleteOrders(o => o.Status == status);
    }

    public async Task DeleteOrdersByYear(int year)
    {
        await _orderRepository.DeleteOrders(o => o.CreatedDate.Year == year);
    }

    public async Task DeleteOrdersByProduct(int productId)
    {
        await _orderRepository.DeleteOrders(o => o.ProductId == productId);
    }
}
