using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ORMFundamentals.Repositories
{
    public abstract class BaseRepository<TContext, T>: IBaseRepository<T> 
        where T : class
        where TContext : DbContext
    {
        protected readonly TContext DbContext;

        private readonly DbSet<T> _dbSet;

        protected BaseRepository(TContext dbContext)
        {
            DbContext = dbContext;
            _dbSet = DbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll(bool trackChanges = true)
        {
            return trackChanges ?
                await _dbSet.ToListAsync() :
                await _dbSet.AsNoTracking().ToListAsync();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToList();
        }

        public async Task DeleteByCondition(Expression<Func<T, bool>> expression)
        {
            var entities = _dbSet.Where(expression);
            _dbSet.RemoveRange(entities);
            await DbContext.SaveChangesAsync();
        }
    }
}
