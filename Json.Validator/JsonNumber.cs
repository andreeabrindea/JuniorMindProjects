namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input) =>
            IsInteger(input) || IsFraction(input) || IsExponentialExpression(input);

        private static bool IsInteger(string number)
        {
            if (IsNegativeInteger(number))
            {
                number = number[1..];
            }

            return AreDigits(number) && !ItStartsWithZero(number);
        }

        private static bool IsFraction(string fraction)
        {
            int indexOfDecimalPoint = GetIndexOfCharacter(fraction, '.');
            return indexOfDecimalPoint > 0
                   && IsDecimalPointPlacedCorrectly(fraction)
                   && AreDigits(fraction[..indexOfDecimalPoint])
                   && AreDigits(fraction[(indexOfDecimalPoint + 1) ..]);
        }

        private static bool IsExponentialExpression(string input)
        {
            if (!HasContent(input))
            {
                return false;
            }

            if ((!input.Contains('e') && !input.Contains('E'))
                || CountCharInString(input, 'E') > 1
                || CountCharInString(input, 'e') > 1)
            {
                return false;
            }

            int indexOfExponent = GetIndexOfExponent(input);
            string expression = GetExpressionAfterExponent(input, indexOfExponent);
            return IsValidExponentialExpression(expression);
        }

        private static bool IsValidExponentialExpression(string expression)
        {
            int signIndex;
            char[] acceptedOperands = { '+', '-' };

            if (expression.Contains('-') || expression.Contains('+'))
            {
                signIndex = expression.IndexOfAny(acceptedOperands);
                expression = expression[(signIndex + 1) ..];
            }

            return AreDigits(expression);
        }

        private static string GetExpressionAfterExponent(string input, int indexOfExponent) => indexOfExponent != -1
            ? input[(indexOfExponent + 1) ..]
            : string.Empty;

        private static int GetIndexOfExponent(string number)
        {
            char[] acceptedExponent = { 'e', 'E' };
            return number.IndexOfAny(acceptedExponent);
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
            return HasContent(input)
                   && input[0] != '.'
                   && input[^1] != '.'
                   && CountCharInString(input, '.') == 1;
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
            return input[0] == '0'
                   && !input.Contains('.')
                   && input.Length > 1;
        }

        private static bool IsNegativeInteger(string input)
        {
            return HasContent(input)
                   && input[0] == '-'
                   && input.Length > 1;
        }
    }
}