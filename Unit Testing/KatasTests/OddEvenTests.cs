using OddEvenKata;

namespace KatasTests;

public class OddEvenTests
{
    private readonly OddEvenTask _oddEvenTask;

    public OddEvenTests()
    {
        _oddEvenTask = new OddEvenTask();
    }

    [Fact]
    public void OddEvenPrimeCheck_1To10_ReturnsExpectedResults()
    {
        //Arrange
        string expectedResult = "Odd\r\nEven\r\n3\r\nEven\r\n5\r\nEven\r\n7\r\nEven\r\nOdd\r\nEven\r\n";

        //Act 
        string result = _oddEvenTask.OddEvenPrimeCheck(1, 10);

        //Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void OddEvenPrimeCheck_11To20_ReturnsExpectedResults()
    {
        //Arrange
        string expectedResult = "11\r\nEven\r\n13\r\nEven\r\nOdd\r\nEven\r\n17\r\nEven\r\n19\r\nEven\r\n";

        //Act
        string result = _oddEvenTask.OddEvenPrimeCheck(11, 20);

        //Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void OddEvenPrimeCheck_21To30_ReturnsExpectedResults()
    {
        //Arrange
        string expectedResult = "Odd\r\nEven\r\n23\r\nEven\r\nOdd\r\nEven\r\nOdd\r\nEven\r\n29\r\nEven\r\n";

        //Act
        string result = _oddEvenTask.OddEvenPrimeCheck(21, 30);

        //Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(2, "Even\r\n")]
    [InlineData(3, "3\r\n")]
    [InlineData(4, "Even\r\n")]
    [InlineData(9, "Odd\r\n")]
    public void OddEvenPrimeCheck_SingleNumber_ReturnsExpectedResult(int number, string expectedResult)
    {
        //Act
        string result = _oddEvenTask.OddEvenPrimeCheck(number, number);

        //Assert
        Assert.Equal(expectedResult, result);
    }
}