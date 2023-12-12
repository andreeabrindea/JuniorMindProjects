namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            int exponentIndex = input.IndexOfAny("eE".ToCharArray());
            int dotIndex = input.IndexOf('.');

            return IsInteger(Integer(input, dotIndex, exponentIndex))
                   && IsFraction(Fraction(input, dotIndex, exponentIndex))
                   && IsExponent(Exponent(input, exponentIndex));
        }

        private static bool IsInteger(string number)
        {
            if (IsNegativeInteger(number))
            {
                number = number[1..];
            }

            return AreDigits(number) && !IsLeadingZero(number);
        }

        private static bool IsFraction(string fraction) => !fraction.StartsWith('.') || AreDigits(fraction[1..]);

        private static bool IsExponent(string input) => (!input.StartsWith('e') && !input.StartsWith('E')) ||
                                                        IsValidExponentialExpression(input[1..]);

        private static string Integer(string input, int dotIndex, int exponentIndex)
        {
            if (dotIndex > -1)
            {
                return input[..dotIndex];
            }

            if (exponentIndex > -1)
            {
                return input[..exponentIndex];
            }

            return input;
        }

        private static string Fraction(string input, int dotIndex, int exponentIndex)
        {
            if (dotIndex > -1 && exponentIndex < 0)
            {
                return input[dotIndex ..];
            }

            if (dotIndex > -1 && exponentIndex > -1)
            {
                return input[dotIndex ..exponentIndex];
            }

            return "0";
        }

        private static bool IsValidExponentialExpression(string expression)
        {
            int signIndex;

            if (expression.StartsWith('-') || expression.StartsWith('+'))
            {
                signIndex = expression.IndexOfAny("+-".ToCharArray());
                expression = expression[(signIndex + 1) ..];
            }

            return AreDigits(expression);
        }

        private static string Exponent(string input, int indexOfExponent) =>
            indexOfExponent != -1 ? input[indexOfExponent ..] : "0";

        private static bool AreDigits(string number)
        {
            foreach (var digit in number)
            {
                if (!char.IsDigit(digit))
                {
                    return false;
                }
            }

            return number.Length > 0;
        }

        private static bool IsLeadingZero(string input) =>
            input.StartsWith('0') && input.Length > 1;

        private static bool IsNegativeInteger(string input) => input[0] == '-' && input.Length > 1;
    }
}