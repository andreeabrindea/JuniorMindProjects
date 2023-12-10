using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return IsInteger(input) || IsFraction(input) || IsExponentialExpression(input);
        }

        private static bool IsInteger(string number)
        {
            if (IsNegativeInteger(number))
            {
                return AreDigits(number[1..]) && !ItStartsWithZero(number[1..]);
            }

            return AreDigits(number) && !ItStartsWithZero(number);
        }

        private static bool IsFraction(string fraction)
        {
            int indexOfDecimalPoint = GetIndexOfCharacter(fraction, '.');
            if (indexOfDecimalPoint < 0)
            {
                return false;
            }

            return IsDecimalPointPlacedCorrectly(fraction) && AreDigits(fraction[..indexOfDecimalPoint]) && AreDigits(fraction[(indexOfDecimalPoint + 1) ..]);
        }

        private static bool IsExponentialExpression(string input)
        {
            if (!HasContent(input))
            {
                return false;
            }

            if ((!input.Contains('e') && !input.Contains('E')) || (CountCharInString(input, 'E') > 1 || CountCharInString(input, 'e') > 1))
            {
                return false;
            }

            int indexOfExponent = GetIndexOfExponent(input);
            string expression = GetExponent(input, indexOfExponent);
            return IsValidExponentialExpression(expression);
        }

        private static bool IsValidExponentialExpression(string expression)
        {
            int signIndex;
            if (expression.Contains('+'))
            {
                signIndex = expression.IndexOf('+');
                return AreDigits(expression[(signIndex + 1) ..]);
            }

            if (expression.Contains('-'))
            {
                signIndex = expression.IndexOf('-');
                return AreDigits(expression[(signIndex + 1) ..]);
            }

            return AreDigits(expression[1..]);
        }

        private static string GetExponent(string input, int indexOfExponent)
        {
            return indexOfExponent != -1
                ? input[indexOfExponent..]
                : string.Empty;
        }

        private static int GetIndexOfExponent(string number)
        {
            if (number.IndexOf('e') != -1)
            {
                return number.IndexOf('e');
            }

            if (number.IndexOf('E') != -1)
            {
                return number.IndexOf('E');
            }

            return -1;
        }

        private static int GetIndexOfCharacter(string input, char c)
        {
            if (!HasContent(input))
            {
                return -1;
            }

            return input.IndexOf(c);
        }

        private static bool IsDecimalPointPlacedCorrectly(string input)
        {
            return HasContent(input) && input[0] != '.' && input[^1] != '.' && CountCharInString(input, '.') == 1;
        }

        private static int CountCharInString(string input, char c)
        {
            int count = 0;
            foreach (char character in input)
            {
                if (character == c)
                {
                    count++;
                }
            }

            return count;
        }

        private static bool AreDigits(string number)
        {
            if (!HasContent(number))
            {
                return false;
            }

            foreach (var digit in number)
            {
                if (!IsDigit(digit))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsDigit(char c)
        {
            int digit = c - '0';
            const int smallestDigit = 0;
            const int biggestDigit = 9;
            return digit >= smallestDigit && digit <= biggestDigit;
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private static bool ItStartsWithZero(string input)
        {
            return input[0] == '0' && !input.Contains('.') && input.Length > 1;
        }

        private static bool IsNegativeInteger(string input)
        {
            return HasContent(input) && input[0] == '-' && input.Length > 1;
        }
    }
}