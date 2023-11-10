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
            const int MaxSingleDigit = 9;
            const int MaxTenthPlaceDigitForPositive = 7;
            const int MaxTenthPlaceDigitForNegative = 8;
            const int TenthPlaceDivisor = 10;
            const string InvalidCharacterErrorMessage = "Invalid character found in the input string.";
            const string OverflowErrorMessage = "Arithmetic operation resulted in an overflow.";

            int result = 0;

            for (int i = startIndex; i < stringValue.Length; i++)
            {
                char currentChar = stringValue[i];

                if (currentChar < '0' || currentChar > '9')
                {
                    throw new FormatException(InvalidCharacterErrorMessage);
                }

                int currentDigit = currentChar - '0';
                bool isCurrentValueExceedsLimitForPositive = !isNegative && currentDigit > MaxTenthPlaceDigitForPositive;
                bool isCurrentValueExceedsLimitForNegative = isNegative && currentDigit > MaxTenthPlaceDigitForNegative;
                bool isCurrentDigitInvalid = currentDigit < 0 || currentDigit > MaxSingleDigit ||
                    (!isCurrentValueExceedsLimitForNegative && result == int.MaxValue / TenthPlaceDivisor) ||
                    (!isCurrentValueExceedsLimitForPositive && result == int.MaxValue / TenthPlaceDivisor) ||
                    result > int.MaxValue / TenthPlaceDivisor;

                if (isCurrentDigitInvalid)
                {
                    throw new OverflowException(OverflowErrorMessage);
                }

                result = result * 10 + currentDigit;
            }

            return result;
        }
    }
}