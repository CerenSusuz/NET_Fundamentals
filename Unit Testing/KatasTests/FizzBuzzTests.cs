using FizzBuzzKata;

namespace KatasTests
{
    public class FizzBuzzTests
    {
        private readonly FizzBuzzTask _fizzBuzz;

        public FizzBuzzTests()
        {
            _fizzBuzz = new FizzBuzzTask();
        }

        [Theory]
        [InlineData(1, "1")]
        [InlineData(3, "Fizz")]
        [InlineData(5, "Buzz")]
        [InlineData(6, "Fizz")]
        [InlineData(10, "Buzz")]
        [InlineData(15, "FizzBuzz")]
        [InlineData(100, "Buzz")]
        public void ComputeFizzBuzz_WithinValidRange_ReturnsCorrectFizzBuzzResult(int number, string expectedResult)
        {
            // Act
            var result = _fizzBuzz.ComputeFizzBuzz(number);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(101)]
        public void ComputeFizzBuzz_OutsideValidRange_ThrowsArgumentOutOfRangeException(int number)
        {
            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _fizzBuzz.ComputeFizzBuzz(number));
        }
    }
}