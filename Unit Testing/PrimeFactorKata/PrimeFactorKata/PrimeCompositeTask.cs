using System.Text;

namespace PrimeFactorKata;

public class PrimeCompositeTask
{
    public bool IsPrime(int number)
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

    public bool IsComposite(int number)
    {
        if (number < 4)
        {  
            return false; 
        }

        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsEven(int number)
    {
        return number % 2 == 0;
    }

    public string PrimeCompositeCheck(int start, int end)
    {
        StringBuilder result = new();

        for (int number = start; number <= end; number++)
        {
            if (IsPrime(number))
            {
                result.AppendLine("Prime");
            }
            else if (IsComposite(number) && !IsEven(number))
            {
                result.AppendLine("Composite");
            }
            else
            {
                result.AppendLine(number.ToString());
            }
        }

        return result.ToString();
    }
}
