using AdoNetFundamentals.DLL.Repositories;
using AdoNetFundamentals.Entities;
using Moq;
using Xunit;

namespace ADONETTests
{
    public class ProductTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly Product _product;

        public ProductTests()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _product = new Product()
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test Description",
                Weight = 5.5,
                Height = 10.0,
                Width = 20.0,
                Lenght = 30.0
            };

            _repositoryMock.Setup(r => r.ReadProduct(1)).Returns(_product);
            _repositoryMock.Setup(r => r.GetAllProducts()).Returns(new List<Product> { _product });
        }

        [Fact]
        public void ReadProduct_Test()
        {
            // Act
            var fetchedProduct = _repositoryMock.Object.ReadProduct(_product.Id);

            // Assert
            Assert.NotNull(fetchedProduct);
            Assert.Equal(_product.Name, fetchedProduct.Name);
        }

        [Fact]
        public void UpdateProduct_Test()
        {
            // Act
            _repositoryMock.Object.UpdateProduct(_product);

            // Assert
            _repositoryMock.Verify(r => r.UpdateProduct(_product), Times.Once());
        }

        [Fact]
        public void DeleteProduct_Test()
        {
            // Act
            _repositoryMock.Object.DeleteProduct(_product.Id);

            // Assert
            _repositoryMock.Verify(r => r.DeleteProduct(_product.Id), Times.Once());
        }

        [Fact]
        public void GetAllProducts_Test()
        {
            // Act
            var products = _repositoryMock.Object.GetAllProducts();

            // Assert
            Assert.NotEmpty(products);
        }
    }
}