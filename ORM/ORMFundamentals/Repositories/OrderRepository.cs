using Microsoft.EntityFrameworkCore;
using ORMFundamentals.Data;
using ORMFundamentals.Entities;
using System.Linq.Expressions;

namespace ORMFundamentals.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order> GetOrderById(int id)
    {
        return await _context.Orders.FindAsync(id);
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

    public async Task<IEnumerable<Order>> GetOrdersByCondition(Expression<Func<Order, bool>> expression)
    {
        return await _context.Orders.Where(expression).ToListAsync();
    }

    public async Task DeleteOrders(Expression<Func<Order, bool>> expression)
    {
        var orders = _context.Orders.Where(expression);
        _context.Orders.RemoveRange(orders);
        await _context.SaveChangesAsync();
    }
}
