using ORMFundamentals.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORMFundamentals.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();

        Product GetProductById(int id);

        Task AddProduct(Product product);

        Task UpdateProduct(Product product);

        Task DeleteProduct(int id);
    }
}
