using ORMFundamentals.Entities;
using System.Linq.Expressions;

namespace ORMFundamentals.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrders();

    Task<Order> GetOrderById(int id);

    Task AddOrder(Order order);

    Task UpdateOrder(Order order);

    Task DeleteOrder(int id);

    Task<IEnumerable<Order>> GetOrdersByCondition(Expression<Func<Order, bool>> expression);

    Task DeleteOrders(Expression<Func<Order, bool>> expression);
}
