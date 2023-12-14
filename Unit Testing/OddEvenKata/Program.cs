using OddEvenKata;

class Program
{
    static void Main(string[] args)
    {
        var oddEvenTask = new OddEvenTask();
        string result = oddEvenTask.OddEvenPrimeCheck(1, 100);

        Console.WriteLine(result);
    }
}