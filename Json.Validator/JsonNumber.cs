using System.Linq;

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

            return AreDigits(number) && !ItStartsWithZeroAndHasMoreThan1Digit(number);
        }

        private static bool IsFraction(string fraction)
        {
            if (string.IsNullOrEmpty(fraction))
            {
                return false;
            }

            int indexOfDecimalPoint = fraction.IndexOf('.');

            return indexOfDecimalPoint > 0
                   && AreDigits(fraction[..indexOfDecimalPoint])
                   && AreDigits(fraction[(indexOfDecimalPoint + 1) ..]);
        }

        private static bool IsExponentialExpression(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            if ((!input.Contains('e') && !input.Contains('E'))
                || input.Count(c => c == 'E') > 1
                || input.Count(c => c == 'e') > 1)
            {
                return false;
            }

            int indexOfExponent = input.IndexOfAny("eE".ToCharArray());
            string expression = GetExpressionAfterExponent(input, indexOfExponent);
            return IsValidExponentialExpression(expression);
        }

        private static bool IsValidExponentialExpression(string expression)
        {
            int signIndex;
            char[] acceptedOperands = { '+', '-' };

            if (expression.StartsWith('-') || expression.StartsWith('+'))
            {
                signIndex = expression.IndexOfAny(acceptedOperands);
                expression = expression[(signIndex + 1) ..];
            }

            return AreDigits(expression);
        }

        private static string GetExpressionAfterExponent(string input, int indexOfExponent)
        {
            if (input.Contains('e') || input.Contains('E'))
            {
                return indexOfExponent != -1
                    ? input[(indexOfExponent + 1) ..]
                    : string.Empty;
            }

            return string.Empty;
        }

        private static bool AreDigits(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }

            foreach (var digit in number)
            {
                if (!char.IsDigit(digit))
                {
                    return false;
                }
            }

            return number.Length > 0;
        }

        private static bool ItStartsWithZeroAndHasMoreThan1Digit(string input) => input.StartsWith('0') && input.Length > 1;

        private static bool IsNegativeInteger(string input) => !string.IsNullOrEmpty(input)
                                                               && input[0] == '-'
                                                               && input.Length > 1;
    }
}