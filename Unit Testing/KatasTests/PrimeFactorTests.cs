using PrimeFactorKata;

namespace KatasTests
{
    public class PrimeFactorTests
    {
        private readonly PrimeCompositeTask _primeCompositeTask;

        public PrimeFactorTests()
        {
            _primeCompositeTask = new PrimeCompositeTask();
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(4, false)]
        [InlineData(102, false)]
        public void IsPrime_Inputs_ReturnsExpectedResults(int number, bool expectedResult)
        {
            // Act
            bool result = _primeCompositeTask.IsPrime(number);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(3, false)]
        [InlineData(4, true)]
        [InlineData(9, true)]
        [InlineData(103, false)]
        public void IsComposite_Inputs_ReturnsExpectedResults(int number, bool expectedResult)
        {
            // Act
            bool result = _primeCompositeTask.IsComposite(number);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        [InlineData(4, true)]
        [InlineData(105, false)]
        public void IsEven_Inputs_ReturnsExpectedResults(int number, bool expectedResult)
        {
            // Act
            bool result = _primeCompositeTask.IsEven(number);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void PrimeCompositeCheck_Input1To5_ReturnsExpectedResults()
        {
            var expectedResult = "1\r\nPrime\r\nPrime\r\n4\r\nPrime\r\n";

            // Act
            var result = _primeCompositeTask.PrimeCompositeCheck(1, 5);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}