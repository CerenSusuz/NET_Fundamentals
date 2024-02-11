namespace ORMFundamentals.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }

        IProductRepository Products { get; }

        Task<int> SaveChangesAsync();
    }
}

