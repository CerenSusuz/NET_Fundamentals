using FizzBuzzKata;

namespace FizzBuzz;

class Program
{
    static void Main(string[] args)
    {
        var fizzBuzz = new FizzBuzzTask();
        PrintFizzBuzzResults(fizzBuzz);
    }

    private static void PrintFizzBuzzResults(FizzBuzzTask fizzBuzz)
    {
        for (int number = 1; number <= 100; number++)
        {
            string result = fizzBuzz.ComputeFizzBuzz(number);
            Console.WriteLine(result);
        }
    }
}