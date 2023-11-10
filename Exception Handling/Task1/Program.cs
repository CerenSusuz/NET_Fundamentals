using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string QuitCommand = "q";
            const string EmptyInputErrorMessage = "Empty input is not allowed!";

            Console.WriteLine("Enter input lines (Enter 'q' to exit):");

            while (true)
            {
                try
                {
                    string input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        throw new ArgumentException(EmptyInputErrorMessage);
                    }

                    if (input.ToLower() == QuitCommand)
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