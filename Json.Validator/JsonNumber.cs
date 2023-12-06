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
                if (!char.IsDigit(t) && t != '.')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
