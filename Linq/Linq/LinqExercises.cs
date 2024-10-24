namespace Linq;

public static class LinqExercises
{
    public static (int consonants, int vowels) GetNoOfConsonantsAndVowels(this string s) =>
        (s.Count(character => !"aeiou".Contains(character) && char.IsLetter(character)),
            s.Count(character => "aeiou".Contains(character) && char.IsLetter(character)));

    public static char GetFirstCharacterThatDoesNotRepeat(this string s) =>
        s.ToLookup(p => p).First(p => p.Count() == 1).Key;

    public static int ConvertStringToInt(this string s)
    {
        const int ten = 10;
        var result = s.Aggregate(0, (accumulate, character) =>
        {
            if (character is < '0' or > '9')
            {
                throw new FormatException(nameof(s));
            }

            return accumulate * ten + (character - '0');
        });
        return GetSign(s) * result;
    }

    public static int GetCharacterWithMaximumNoOfOccurrences(this string s) =>
        s.GroupBy(p => p).MaxBy(p => p.Count())!.Key;

    private static int GetSign(this string s) => s[0] == '-' ? -1 : 1;
}