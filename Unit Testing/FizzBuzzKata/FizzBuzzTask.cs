namespace FizzBuzzKata
{
    public class FizzBuzzTask
    {
        public string ComputeFizzBuzz(int number)
        {
            if (number < 1 || number > 100)
            {
                throw new ArgumentOutOfRangeException();
            }
                
            var result = string.Empty;

            if (IsDivisibleBy(number, 3))
            {
                result += "Fizz";
            }

            if (IsDivisibleBy(number, 5))
            {
                result += "Buzz";
            }

            return string.IsNullOrEmpty(result) ? number.ToString() : result;
        }

        private bool IsDivisibleBy(int number, int divisor)
        {
            return number % divisor == 0;
        }
    }
}
