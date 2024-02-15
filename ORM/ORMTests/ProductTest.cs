using Moq;
using ORMFundamentals.Entities;
using ORMFundamentals.Repositories;
using ORMFundamentals.Services;

namespace ORMTests;

public class ProductTests
{
    private readonly Mock<IProductRepository> _mockProductRepo;
    private readonly ProductService _productService;

    public ProductTests()
    {
        _mockProductRepo = new Mock<IProductRepository>();
        _productService = new ProductService(_mockProductRepo.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsAllProducts()
    {
        // Arrange
        _mockProductRepo.Setup(repo => repo.GetAllProducts())
            .ReturnsAsync(GetTestProducts());

        // Act
        var result = await _productService.GetAll();

        // Assert
        Assert.Equal(3, result.Count());
        Assert.IsAssignableFrom<IEnumerable<Product>>(result);
    }

    [Fact]
    public async Task GetProductById_ReturnsProduct()
    {
        // Arrange
        var testProduct = new Product() { Id = 1, Name = "TestProduct" };
        _mockProductRepo.Setup(repo => repo.GetProductById(1))
            .ReturnsAsync(testProduct);

        // Act
        var result = await _productService.GetProductById(1);

        // Assert
        Assert.Equal(1, result.Id);
        Assert.Equal("TestProduct", result.Name);
    }

    [Fact]
    public async Task Add_AddsNewProduct_WhenProductIsValid()
    {
        // Arrange
        var newProduct = new Product() { Name = "New Product" };
        _mockProductRepo.Setup(repo => repo.AddProduct(newProduct))
                        .Returns(Task.CompletedTask);

        // Act
        await _productService.Add(newProduct);

        // Assert
        _mockProductRepo.Verify(repo => repo.AddProduct(newProduct), Times.Once);
    }

    [Fact]
    public async Task Update_UpdatesExistingProduct_WhenProductIsValid()
    {
        // Arrange
        var existingProduct = new Product() { Id = 1, Name = "Updated Product" };
        _mockProductRepo.Setup(repo => repo.UpdateProduct(existingProduct))
                        .Returns(Task.CompletedTask);

        // Act
        await _productService.Update(existingProduct);

        // Assert
        _mockProductRepo.Verify(repo => repo.UpdateProduct(existingProduct), Times.Once);
    }

    [Fact]
    public async Task Delete_DeletesProductById_WhenIdIsValid()
    {
        // Arrange
        var id = 1;
        _mockProductRepo.Setup(repo => repo.DeleteProduct(id))
                        .Returns(Task.CompletedTask);

        // Act
        await _productService.Delete(id);

        // Assert
        _mockProductRepo.Verify(repo => repo.DeleteProduct(id), Times.Once);
    }

    private List<Product> GetTestProducts()
    {
        return
        [
            new Product() { Id = 1, Name = "Test Product 1" },
            new Product() { Id = 2, Name = "Test Product 2" },
            new Product() { Id = 3, Name = "Test Product 3" },
        ];
    }
}