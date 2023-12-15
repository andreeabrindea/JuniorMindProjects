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

            input = input[1.. ^1];

            if (input.EndsWith('\\'))
            {
                return false;
            }

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i - 1] == '\\')
                {
                    return IsEscape(input[i], input);
                }

                if (!IsCharacter(input[i]))
                {
                    return false;
                }
            }

            return true;
        }

       private static bool IsCharacter(char character) => character >= ' ';

       private static bool IsEscape(char character, string input)
        {
            switch (character)
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
           int indexOfCharacterU = input.IndexOf("u");
           const int escapeSequenceLength = 5;
           int endIndex = indexOfCharacterU + escapeSequenceLength;
           if (input.Length < endIndex)
           {
               return false;
           }

           input = input[(indexOfCharacterU + 1) ..endIndex];
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