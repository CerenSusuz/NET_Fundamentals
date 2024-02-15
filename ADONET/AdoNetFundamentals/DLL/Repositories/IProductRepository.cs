using AdoNetFundamentals.Entities;

namespace AdoNetFundamentals.DLL.Repositories;

public interface IProductRepository
{
    Product ReadProduct(int id);

    void UpdateProduct(Product product);

    void DeleteProduct(int id);

    List<Product> GetAllProducts();
}
