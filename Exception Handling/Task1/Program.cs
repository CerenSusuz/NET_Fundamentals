using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter input lines (Enter 'q' to exit):");

            while (true)
            {
                try
                {
                    string input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        throw new ArgumentException("Empty input is not allowed!");
                    }

                    if (input.ToLower() == "q")
                    {
                        break;
                    }

                    char firstChar = GetFirstCharacter(input);

                    Console.WriteLine($"First character: {firstChar}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            Console.WriteLine("Program terminated.");
        }

        private static char GetFirstCharacter(string input)
        {
            return input[0];
        }
    }
}