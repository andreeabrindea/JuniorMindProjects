using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            if (IsEndingWithBackslash(input))
            {
                return false;
            }

            return HasContent(input) && StartsAndEndsWithDoubleQuotes(input) && !ContainsControlCharacters(input) && !ContainsUnrecognizedEscapeCharacters(input);
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

        private static bool ContainsUnrecognizedEscapeCharacters(string input)
        {
            const string pattern = @"\\1|\\2|\\3|\\4|\\5|\\6|\\7|\\8|\\9|\\c|\\d|\\e|\\g|\\h|\\i|\\j|\\k|\\l|\\m|\\o|\\p|\\q|\\s|\\w|\\x|\\y|\\z";
            return Regex.IsMatch(input, pattern);
        }

        private static bool IsEndingWithBackslash(string input)
        {
            const string pattern = "\\\\\"$";
            return HasContent(input) && Regex.IsMatch(input, pattern);
        }
    }
}
