using System.Text.RegularExpressions;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input) => IsComplete(input)
                                                         && !IsEndingWithBackslash(input)
                                                         && !ContainsControlCharacters(input)
                                                         && !ContainsUnrecognizedEscapeCharacters(input);

        private static bool IsComplete(string input) => HasContent(input)
                                                        && ContainsCompleteHexadecimalUnicode(input)
                                                        && StartsAndEndsWithDoubleQuotes(input);

        private static bool HasContent(string input) => !string.IsNullOrEmpty(input);

        private static bool StartsAndEndsWithDoubleQuotes(string input) => input[0] == '\"' && input[^1] == '\"';

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
            const string pattern =
                @"\\1|\\2|\\3|\\4|\\5|\\6|\\7|\\8|\\9|\\c|\\d|\\e|\\g|\\h|\\i|\\j|\\k|\\l|\\m|\\o|\\p|\\q|\\s|\\w|\\x|\\y|\\z";
            return Regex.IsMatch(input, pattern);
        }

        private static bool IsEndingWithBackslash(string input)
        {
            const string pattern = "\\\\\"$";
            return HasContent(input) && Regex.IsMatch(input, pattern);
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