using ORMFundamentals.Data;
using ORMFundamentals.Entities;

namespace ORMFundamentals.Repositories
{
    public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
    }
}