using Moq;
using ORMFundamentals.Entities;
using ORMFundamentals.Repositories;
using ORMFundamentals.Services;

namespace ORMTests;

public class OrderTests
{
    private readonly Mock<IOrderRepository> _mockOrderRepo;
    private readonly OrderService _orderService;

    public OrderTests()
    {
        _mockOrderRepo = new Mock<IOrderRepository>();
        _orderService = new OrderService(_mockOrderRepo.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsAllOrders()
    {
        // Arrange
        _mockOrderRepo.Setup(repo => repo.GetAllOrders())
            .ReturnsAsync(GetTestOrders());

        // Act
        var result = await _orderService.GetAll();

        // Assert
        Assert.Equal(3, result.Count());
        Assert.IsAssignableFrom<IEnumerable<Order>>(result);
    }

    [Fact]
    public async Task GetOrderById_ReturnsOrder()
    {
        // Arrange
        var testOrder = new Order() { Id = 1, Status = Status.Loading };
        _mockOrderRepo.Setup(repo => repo.GetOrderById(1))
            .ReturnsAsync(testOrder);

        // Act
        var result = await _orderService.GetOrderById(1);

        // Assert
        Assert.Equal(1, result.Id);
        Assert.Equal(Status.Loading, result.Status);
    }

    [Fact]
    public async Task Add_AddsNewOrder_WhenOrderIsValid()
    {
        // Arrange
        var newOrder = new Order() { Status = Status.Loading, CreatedDate = DateTime.Now };
        _mockOrderRepo.Setup(repo => repo.AddOrder(newOrder))
                      .Returns(Task.CompletedTask);

        // Act
        await _orderService.Add(newOrder);

        // Assert
        _mockOrderRepo.Verify(repo => repo.AddOrder(newOrder), Times.Once);
    }

    [Fact]
    public async Task Update_UpdatesExistingOrder_WhenOrderIsValid()
    {
        // Arrange
        var existingOrder = new Order() { Id = 1, Status = Status.Arrived, CreatedDate = DateTime.Now };
        _mockOrderRepo.Setup(repo => repo.UpdateOrder(existingOrder))
                      .Returns(Task.CompletedTask);

        // Act
        await _orderService.Update(existingOrder);

        // Assert
        _mockOrderRepo.Verify(repo => repo.UpdateOrder(existingOrder), Times.Once);
    }

    [Fact]
    public async Task Delete_DeletesOrderById_WhenIdIsValid()
    {
        // Arrange
        var id = 1;
        _mockOrderRepo.Setup(repo => repo.DeleteOrder(id))
                      .Returns(Task.CompletedTask);

        // Act
        await _orderService.Delete(id);

        // Assert
        _mockOrderRepo.Verify(repo => repo.DeleteOrder(id), Times.Once);
    }

    private List<Order> GetTestOrders()
    {
        return
        [
            new Order() { Id = 1, Status = Status.Loading },
            new Order() { Id = 2, Status = Status.InProgress },
            new Order() { Id = 3, Status = Status.Arrived },
        ];
    }
}