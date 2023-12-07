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

            return StartsAndEndsWithDoubleQuotes(input);
        }

        private static bool StartsAndEndsWithDoubleQuotes(string input)
        {
            return input[0] == '\"' && input[^1] == '\"';
        }
    }
}
