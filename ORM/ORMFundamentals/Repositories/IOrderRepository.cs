using ORMFundamentals.Entities;
using System.Linq.Expressions;

namespace ORMFundamentals.Repositories;
public interface IOrderRepository
{
    IEnumerable<Order> GetAllOrders();

    Order GetOrderById(int id);

    Task AddOrder(Order order);

    Task UpdateOrder(Order order);

    Task DeleteOrder(int id);

    IEnumerable<Order> GetOrdersByCondition(Expression<Func<Order, bool>> expression);

    Task DeleteOrders(Expression<Func<Order, bool>> expression);
}