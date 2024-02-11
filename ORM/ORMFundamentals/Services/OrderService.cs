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

    public IEnumerable<Order> GetAllOrders()
    {
        return _orderRepository.GetAllOrders();
    }

    public Order GetOrderById(int id)
    {
        return _orderRepository.GetOrderById(id);
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

    public IEnumerable<Order> GetOrdersByMonth(int month)
    {
        return _orderRepository.GetOrdersByCondition(order => order.CreatedDate.Month == month);
    }

    public IEnumerable<Order> GetOrdersByStatus(Status status)
    {
        return _orderRepository.GetOrdersByCondition(order => order.Status == status);
    }

    public IEnumerable<Order> GetOrdersByYear(int year)
    {
        return _orderRepository.GetOrdersByCondition(order => order.CreatedDate.Year == year);
    }

    public IEnumerable<Order> GetOrdersByProduct(int productId)
    {
        return _orderRepository.GetOrdersByCondition(order => order.ProductId == productId);
    }

    public async Task DeleteOrdersByMonth(int month)
    {
        await _orderRepository.DeleteOrders(order => order.CreatedDate.Month == month);
    }

    public async Task DeleteOrdersByStatus(Status status)
    {
        await _orderRepository.DeleteOrders(order => order.Status == status);
    }

    public async Task DeleteOrdersByYear(int year)
    {
        await _orderRepository.DeleteOrders(order => order.CreatedDate.Year == year);
    }

    public async Task DeleteOrdersByProduct(int productId)
    {
        await _orderRepository.DeleteOrders(order => order.ProductId == productId);
    }
}
