using System;
using System.Text;

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

            foreach (var t in input)
            {
                if (!IsValidMathematicalSymbols(t))
                {
                    return false;
                }
            }

            return IsValidNegativeNumber(input) && IsValidFloatNumber(input) && IsValidNumber(input);
        }

        private static bool IsValidNumber(string number)
        {
            int countDecimalPoints = CountCharInString(number, '.');
            return number[0] != '0' || countDecimalPoints != 0 || number.Length <= 1;
        }

        private static bool IsValidMathematicalSymbols(char c)
        {
            const string mathematicalSymbols = "0123456789.eE-";
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
            foreach (var i in input)
            {
                if (i == c)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
