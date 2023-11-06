using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            ValidateInput(stringValue);

            stringValue = stringValue.Trim();
            int startIndex = HandleSign(stringValue, out bool isNegative);
            int result = ExtractNumber(stringValue, startIndex, isNegative);

            return isNegative ? -result : result;
        }

        private void ValidateInput(string stringValue)
        {
            if (stringValue == null)
            {
                throw new ArgumentNullException(nameof(stringValue));
            }

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                throw new FormatException("Invalid input: string is empty or white space.");
            }
        }

        private int HandleSign(string stringValue, out bool isNegative)
        {
            isNegative = false;
            int startIndex = 0;

            if (stringValue[0] == '-')
            {
                isNegative = true;
                startIndex = 1;
            }
            else if (stringValue[0] == '+')
            {
                startIndex = 1;
            }

            return startIndex;
        }

        private int ExtractNumber(string stringValue, int startIndex, bool isNegative)
        {
            int result = 0;

            for (int i = startIndex; i < stringValue.Length; i++)
            {
                char currentChar = stringValue[i];

                if (currentChar < '0' || currentChar > '9')
                {
                    throw new FormatException("Invalid character found in the input string.");
                }

                int currentDigit = currentChar - '0';

                if (result > int.MaxValue / 10 || (result == int.MaxValue / 10 && ((!isNegative && currentDigit > 7) || (isNegative && currentDigit > 8))))
                {
                    throw new OverflowException("Arithmetic operation resulted in an overflow.");
                }

                result = result * 10 + currentDigit;
            }

            return result;
        }
    }
}