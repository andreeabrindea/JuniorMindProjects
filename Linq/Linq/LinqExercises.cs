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
        int sign = GetSign(s);
        if ("-+".Any(prefix => s.StartsWith(prefix)))
        {
            s = s[1..];
        }

        var result = s.Aggregate(0, (accumulate, character) =>
        {
            if (character is < '0' or > '9')
            {
                throw new FormatException(nameof(s));
            }

            return accumulate * ten + (character - '0');
        });
        return sign * result;
    }

    public static int GetCharacterWithMaximumNoOfOccurrences(this string s) =>
        s.GroupBy(p => p).MaxBy(p => p.Count())!.Key;

    public static IEnumerable<string> GetPalindromes(this string input) =>
        Enumerable
            .Range(1, input.Length)
            .SelectMany(length => Enumerable.Range(0, input.Length - length + 1)
                .Select(a => input.Substring(a, length)))
            .Where(b => b.SequenceEqual(b.Reverse()) && b.Length > 1)
            .Distinct();

    private static int GetSign(this string s) => s[0] == '-' ? -1 : 1;
}