using ORMFundamentals.Entities;

namespace ORMFundamentals.Services;

public interface IProductService
{
    IEnumerable<Product> GetAllProducts();

    Product GetProductById(int id);

    Task Add(Product product);

    Task Update(Product product);

    Task Delete(int id);
}
