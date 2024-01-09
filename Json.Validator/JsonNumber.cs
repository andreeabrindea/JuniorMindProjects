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
            if (number.StartsWith('-'))
            {
                number = number[1..];
            }

            return AreDigits(number) && !HasLeadingZero(number);
        }

        private static bool IsFraction(string fraction) => string.IsNullOrEmpty(fraction) || AreDigits(fraction[1..]);

        private static bool IsExponent(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }

            input = input[1..];

            if (input.StartsWith('-') || input.StartsWith('+'))
            {
                input = input[1..];
            }

            return AreDigits(input);
        }

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
            if (dotIndex == -1)
            {
                return string.Empty;
            }

            if (dotIndex > -1 && exponentIndex < 0)
            {
                return input[dotIndex ..];
            }

            return input[dotIndex ..exponentIndex];
        }

        private static string Exponent(string input, int indexOfExponent) => indexOfExponent != -1 ? input[indexOfExponent ..] : string.Empty;

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

        private static bool HasLeadingZero(string input) => input.StartsWith('0') && input.Length > 1;
    }
}