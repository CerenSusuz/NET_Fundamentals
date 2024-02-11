using ORMFundamentals.Entities;

namespace ORMFundamentals.Services;

public interface IOrderService
{
    IEnumerable<Order> GetAllOrders();

    Order GetOrderById(int id);

    Task Add(Order order);

    Task Update(Order order);

    Task Delete(int id);

    Task<IEnumerable<Order>> GetOrdersByMonth(int month);

    Task<IEnumerable<Order>> GetOrdersByStatus(Status status);

    Task<IEnumerable<Order>> GetOrdersByYear(int year);

    Task<IEnumerable<Order>> GetOrdersByProduct(int productId);

    Task DeleteOrdersByMonth(int month);

    Task DeleteOrdersByStatus(Status status);

    Task DeleteOrdersByYear(int year);

    Task DeleteOrdersByProduct(int productId);
}
