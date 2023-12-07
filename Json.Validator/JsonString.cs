using System;
using System.ComponentModel;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            return HasContent(input) && StartsAndEndsWithDoubleQuotes(input) && !ContainsControlCharacters(input);
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private static bool StartsAndEndsWithDoubleQuotes(string input)
        {
            return input[0] == '\"' && input[^1] == '\"';
        }

        private static bool ContainsControlCharacters(string input)
        {
            foreach (char c in input)
            {
                if (char.IsControl(c))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
