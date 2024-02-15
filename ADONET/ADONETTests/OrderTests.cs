using AdoNetFundamentals.Entities;
using Moq;

namespace ADONETTests;

public class OrderTests
{
    private readonly Mock<IOrderRepository> _repositoryMock;
    private readonly Order _order;

    public OrderTests()
    {
        _repositoryMock = new Mock<IOrderRepository>();
        _order = new Order()
        {
            Id = 1,
            Status = "Test Status",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            ProductId = 1
        };

        var emptyOrderList = new List<Order> { _order };

        _repositoryMock.Setup(r => r.Read(_order.Id)).Returns(_order);
        _repositoryMock.Setup(r => r.FetchOrdersByFilter(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns(emptyOrderList);
        _repositoryMock.Setup(r => r.BulkDeleteOrders(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));
    }

    [Fact]
    public void ReadOrder_Test()
    {
        // Act
        var fetchedOrder = _repositoryMock.Object.Read(_order.Id);

        // Assert
        Assert.NotNull(fetchedOrder);
        Assert.Equal(_order.Status, fetchedOrder.Status);
    }

    [Fact]
    public void UpdateOrder_Test()
    {
        // Act
        _repositoryMock.Object.Update(_order);

        // Assert
        _repositoryMock.Verify(r => r.Update(_order), Times.Once());
    }

    [Fact]
    public void DeleteOrder_Test()
    {
        // Act
        _repositoryMock.Object.Delete(_order.Id);

        // Assert
        _repositoryMock.Verify(r => r.Delete(_order.Id), Times.Once());
    }

    [Fact]
    public void FetchOrdersByFilter_Test()
    {
        // Act
        var orders = _repositoryMock.Object.FetchOrdersByFilter();

        // Assert
        Assert.NotEmpty(orders);
    }

    [Fact]
    public void BulkDeleteOrders_Test()
    {
        // Act
        _repositoryMock.Object.BulkDeleteOrders(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

        // Assert
        _repositoryMock.Verify(r => r.BulkDeleteOrders(
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>()), Times.Once());
    }
}