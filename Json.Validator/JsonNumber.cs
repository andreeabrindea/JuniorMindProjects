using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return IsValidNumber(input) && IsValidFloatNumber(input) && IsValidNegativeNumber(input) && IsValidExponentialNumber(input);
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private static bool ContainsJustMathematicalSymbols(string input)
        {
            if (!HasContent(input))
            {
                return false;
            }

            foreach (var t in input)
            {
                if (!IsValidMathematicalSymbols(t))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsValidNumber(string number)
        {
            if (!ContainsJustMathematicalSymbols(number))
            {
                return false;
            }

            int countDecimalPoints = CountCharInString(number, '.');
            return number[0] != '0' || countDecimalPoints != 0 || number.Length <= 1;
        }

        private static bool IsValidMathematicalSymbols(char c)
        {
            const string mathematicalSymbols = "0123456789.eE-+";
            return mathematicalSymbols.Contains(c);
        }

        private static bool IsValidNegativeNumber(string number)
        {
            return CountCharInString(number, '-') <= 1;
        }

        private static bool IsValidFloatNumber(string number)
        {
            return CountCharInString(number, '.') <= 1 && number[^1] != '.';
        }

        private static int CountCharInString(string input, char c)
        {
            int count = 0;
            c = char.ToLower(c);
            input = input.ToLower();
            foreach (var i in input)
            {
                if (i == c)
                {
                    count++;
                }
            }

            return count;
        }

        private static bool IsValidExponentialNumber(string number)
        {
            int countExponentialChar = CountCharInString(number, 'e');
            if (countExponentialChar > 1)
            {
                return false;
            }

            int indexOfExponent = -1;
            if (number.IndexOf('e') != -1)
            {
                indexOfExponent = number.IndexOf('e');
            }
            else if (number.IndexOf('E') != -1)
            {
                indexOfExponent = number.IndexOf('E');
            }

            if (indexOfExponent < 0)
            {
                return true;
            }

            string afterExponent = number.Substring(indexOfExponent + 1);
            if (afterExponent.Contains("."))
            {
                return false;
            }

            return IsSignFollowedByDigit(number) && indexOfExponent != number.Length - 1;
        }

        private static bool IsSignFollowedByDigit(string number)
        {
            int indexOfSignPlus = number.IndexOf('+');
            int indexOfSignMinus = number.IndexOf('-');

            return indexOfSignMinus != number.Length - 1 && indexOfSignPlus != number.Length - 1;
        }
    }
}
