using System.Linq.Expressions;

namespace ORMFundamentals.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAll(bool trackChanges = true);

        T GetById(int id);

        Task Add(T entity);

        Task Update(T entity);

        Task Delete(int id);

        IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression);

        Task DeleteByCondition(Expression<Func<T, bool>> expression);
    }
}
