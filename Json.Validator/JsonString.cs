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

            if (input.Length == 2)
            {
                return true;
            }

            return AreCharacters(input);
        }

       private static bool IsDoubleQuoted(string input) => input.StartsWith('\"') && input.EndsWith('\"');

       private static bool AreCharacters(string input)
        {
            if (!IsDoubleQuoted(input))
            {
                return false;
            }

            int endIndex = input.Length - 2;
            input = input[.. (endIndex + 1)];

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

                if (!IsCharacter(input[i]) && !IsEscape(input[i], input))
                {
                    return false;
                }
            }

            return true;
        }

       private static bool IsCharacter(char c)
        {
            const int lowerBound = 32;
            return Convert.ToInt32(c) >= lowerBound && c != '\"' && c != '\\';
        }

       private static bool IsEscape(char c, string input)
        {
            switch (c)
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
           const int unicodeLength = 5;
           int endIndex = indexOfCharacterU + unicodeLength;
           if (input.Length < endIndex)
           {
               return false;
           }

           input = input[(indexOfCharacterU + 1) ..endIndex];
           foreach (var c in input)
           {
               if (!IsHex(c))
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