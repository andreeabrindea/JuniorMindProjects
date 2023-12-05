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

            const int BiggestDigit = 9;
            for (int i = 0; i < input.Length; i++)
            {
                int digit = input[i] - '0';
                if (digit < 0 || digit > BiggestDigit)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
