using System;
using System.Text.RegularExpressions;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            if (string.IsNullOrEmpty(input) || !IsDoubleQuoted(input))
            {
                return false;
            }

            return AreCharacters(input)
                   && !IsEndingWithBackslash(input)
                   && !ContainsUnrecognizedEscapeCharacters(input)
                   && ContainsCompleteHexadecimalUnicode(input);
        }

        private static bool IsDoubleQuoted(string input) => input.StartsWith('\"') && input.EndsWith('\"');

        private static bool AreCharacters(string input)
        {
            foreach (var c in input)
            {
                if (!IsCharacter(c) || char.IsControl(c))
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

        private static bool ContainsUnrecognizedEscapeCharacters(string input)
        {
            string[] patterns =
            {
                "\\1", "\\2", "\\3", "\\4", "\\5", "\\6", "\\7", "\\8", "\\9",
                "\\c", "\\d", "\\e", "\\g", "\\h", "\\i", "\\j", "\\k", "\\l",
                "\\m", "\\o", "\\p", "\\q", "\\s", "\\w", "\\x", "\\y", "\\z"
            };

            foreach (string pattern in patterns)
            {
                if (input.Contains(pattern))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsEndingWithBackslash(string input)
        {
            int indexOfLastElement = input.Length - 2;
            return input[indexOfLastElement] == '\\';
        }

        private static bool ContainsCompleteHexadecimalUnicode(string input)
        {
            if (!input.Contains("\\u"))
            {
                return true;
            }

            const string pattern = @"\\u[0-9a-fA-F]{4}(?![0-9a-fA-F])";
            return Regex.IsMatch(input, pattern);
        }
    }
}