using ORMFundamentals.Entities;
using ORMFundamentals.Repositories;

namespace ORMFundamentals.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _productRepository.GetAllProducts();
    }

    public Product GetProductById(int id)
    {
        return _productRepository.GetProductById(id);
    }

    public async Task Add(Product product)
    {
        await _productRepository.AddProduct(product);
    }

    public async Task Update(Product product)
    {
        await _productRepository.UpdateProduct(product);
    }

    public async Task Delete(int id)
    {
        await _productRepository.DeleteProduct(id);
    }
}
