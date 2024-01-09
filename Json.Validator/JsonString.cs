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

            return IsEmptyDoubleQuotedString(input) || AreCharacters(input);
        }

        private static bool IsDoubleQuoted(string input) => input.StartsWith('\"') && input.EndsWith('\"');

        private static bool IsEmptyDoubleQuotedString(string input) => input.Length == 2 && IsDoubleQuoted(input);

        private static bool AreCharacters(string input)
        {
            if (!IsDoubleQuoted(input))
            {
                return false;
            }

            input = input[1..^1];

            int i = 0;
            const int skipTwoChars = 2;
            while (i < input.Length)
            {
                if ((input[i] == '\\' && i + 1 == input.Length) || input[i] == '\\' && !IsEscape(input.Substring(i + 1)))
                {
                    return false;
                }

                if (input[i] == '\\' && input[i + 1] == '\\')
                {
                    i += skipTwoChars;
                    continue;
                }

                if (!IsCharacter(input[i]))
                {
                    return false;
                }

                ++i;
            }

            return true;
        }

        private static bool IsCharacter(char character) => character >= ' ';

        private static bool IsEscape(string input)
        {
            switch (input[0])
            {
                case '\"':
                case '\\':
                case '/':
                case 'b':
                case 'f':
                case 'r':
                case 't':
                case 'n':
                    return true;
                case 'u':
                    return IsCompleteHexadecimalUnicode(input);
                default:
                    return false;
            }
        }

        private static bool IsCompleteHexadecimalUnicode(string input)
        {
            const int escapeSequenceLength = 5;
            if (input.Length < escapeSequenceLength)
            {
                return false;
            }

            input = input[1..escapeSequenceLength];
            foreach (var character in input)
            {
                if (!IsHex(character))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsHex(char character)
        {
            character = char.ToLower(character);
            return character >= '0' && character <= '9' || character >= 'a' && character <= 'f';
        }
    }
}
