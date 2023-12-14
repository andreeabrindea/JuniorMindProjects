using System;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return AreCharacters(input) && !IsEndingWithBackslash(input);
        }

        private static bool IsDoubleQuoted(string input) => input.StartsWith('\"') && input.EndsWith('\"');

        private static bool AreCharacters(string input)
        {
            if (!IsDoubleQuoted(input))
            {
                return false;
            }

            foreach (var c in input)
            {
                if (!IsCharacter(c) && !IsEscape(c) || char.IsControl(c))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsCharacter(char c)
        {
            const int lowerBound = 32;
            return Convert.ToInt32(c) >= lowerBound;
        }

        private static bool IsEscape(char c)
        {
            if (c.ToString().StartsWith("\\u"))
            {
                return IsCompleteHexadecimalUnicode(c);
            }

            const string pattern = "\\\"\b\f\n\r\t\\/";
            return pattern.Contains(c);
        }

        private static bool IsEndingWithBackslash(string input)
        {
            int indexOfLastElement = input.Length - 2;
            return input[indexOfLastElement] == '\\';
        }

        private static bool IsCompleteHexadecimalUnicode(char c)
        {
            string escapeUnicodeString = c.ToString();
            if (escapeUnicodeString.Length < 6)
            {
                return false;
            }

            if (!escapeUnicodeString.StartsWith("\\u"))
            {
                return false;
            }

            const int startIndexForHex = 2;
            foreach (var character in escapeUnicodeString[startIndexForHex..])
            {
                if (!IsHex(character))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsHex(char c)
        {
            c = char.ToLower(c);
            return c >= '0' && c <= '9' || c >= 'a' && c <= 'f';
        }
    }
}