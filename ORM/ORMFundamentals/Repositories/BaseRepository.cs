using Microsoft.EntityFrameworkCore;
using ORMFundamentals.Data;
using System.Linq.Expressions;

namespace ORMFundamentals.Repositories
{
    public abstract class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context = context;

        public async Task<IEnumerable<T>> GetAll(bool trackChanges = true)
        {
            return trackChanges ?
                await _context.Set<T>().ToListAsync() :
                await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).ToList();
        }

        public async Task DeleteByCondition(Expression<Func<T, bool>> expression)
        {
            var entities = _context.Set<T>().Where(expression);
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
