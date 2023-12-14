using System;
using PrimeFactorKata;

class Program
{
    static void Main(string[] args)
    {
        var primeComposite = new PrimeCompositeTask();
        string result = primeComposite.PrimeCompositeCheck(1, 100);

        Console.WriteLine(result);
    }
}