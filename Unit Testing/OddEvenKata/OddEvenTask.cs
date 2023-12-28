using System.Text;

namespace OddEvenKata
{
    public class OddEvenTask
    {
        private bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }
            else if (number == 2)
            {
                return true;
            }
            else if (number % 2 == 0)
            {
                return false;
            }
            else
            {
                for (int i = 3; i * i <= number; i += 2)
                {
                    if (number % i == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        private bool IsOdd(int number)
        {
            return number % 2 != 0;
        }

        public string OddEvenPrimeCheck(int start, int end)
        {
            StringBuilder result = new StringBuilder();

            for (int number = start; number <= end; number++)
            {
                if (IsEven(number))
                {
                    result.AppendLine("Even");
                }
                else if (IsOdd(number) && !IsPrime(number))
                {
                    result.AppendLine("Odd");
                }
                else if (IsPrime(number))
                {
                    result.AppendLine(number.ToString());
                }
            }

            return result.ToString();
        }
    }
}
