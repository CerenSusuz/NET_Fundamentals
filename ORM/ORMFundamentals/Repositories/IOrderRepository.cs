using ORMFundamentals.Entities;
using System.Linq.Expressions;

namespace ORMFundamentals.Repositories;
public interface IOrderRepository : IBaseRepository<Order>
{
    IEnumerable<Order> GetOrdersByCondition(Expression<Func<Order, bool>> expression);

    Task DeleteOrders(Expression<Func<Order, bool>> expression);
}