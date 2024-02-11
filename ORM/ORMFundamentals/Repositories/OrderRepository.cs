using ORMFundamentals.Data;
using ORMFundamentals.Entities;
using System.Linq.Expressions;

namespace ORMFundamentals.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.Find(id);
        }

        public async Task AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Order> GetOrdersByCondition(Expression<Func<Order, bool>> expression)
        {
            return _context.Orders.Where(expression).ToList();
        }

        public async Task DeleteOrders(Expression<Func<Order, bool>> expression)
        {
            var orders = _context.Orders.Where(expression);
            _context.Orders.RemoveRange(orders);
            await _context.SaveChangesAsync();
        }
    }
}
