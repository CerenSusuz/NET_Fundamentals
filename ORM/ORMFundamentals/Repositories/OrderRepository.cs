using ORMFundamentals.Data;
using ORMFundamentals.Entities;
using System.Linq.Expressions;

namespace ORMFundamentals.Repositories
{
    public class OrderRepository(AppDbContext context) : BaseRepository<Order>(context), IOrderRepository
    {
        public IEnumerable<Order> GetOrdersByCondition(Expression<Func<Order, bool>> expression)
        {
            return GetByCondition(expression);
        }

        public async Task DeleteOrders(Expression<Func<Order, bool>> expression)
        {
            await DeleteByCondition(expression);
        }
    }
}
