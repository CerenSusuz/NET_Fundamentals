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

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _productRepository.GetAllProducts();
    }

    public async Task<Product> GetProductById(int id)
    {
        return await _productRepository.GetProductById(id);
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
