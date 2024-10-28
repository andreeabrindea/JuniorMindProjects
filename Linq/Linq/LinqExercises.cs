namespace Linq;

public static class LinqExercises
{
    public static (int Consonants, int Vowels) GetNoOfConsonantsAndVowels(this string input) =>
        (input.Count(character => !"aeiou".Contains(character) && char.IsLetter(character)),
            input.Count(character => "aeiou".Contains(character) && char.IsLetter(character)));

    public static char GetFirstCharacterThatDoesNotRepeat(this string input) =>
        input.ToLookup(p => p).First(p => p.Count() == 1).Key;

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

    public static int GetCharacterWithMaximumNoOfOccurrences(this string input) =>
        input.GroupBy(p => p).MaxBy(p => p.Count())!.Key;

    public static IEnumerable<string> GetPalindromes(this string input) =>
        Enumerable
            .Range(1, input.Length)
            .SelectMany(length => Enumerable.Range(0, input.Length - length + 1)
                .Select(a => input.Substring(a, length)))
            .Where(b => b.SequenceEqual(b.Reverse()) && b.Length > 1)
            .Distinct();

    public static IEnumerable<string> GenerateSum(this int n, int k) =>
        Enumerable.Range(0, 1 << n)
            .Select(bits =>
            {
                List<int> permutation = Enumerable.Range(0, n)
                    .Select(p => (bits & (1 << p)) != 0 ? (p + 1) : -(p + 1))
                    .ToList();

                int sum = permutation.Sum();
                string representation = string.Join(" + ", permutation);

                return new { sum, representation };
            })
            .Where(intermediate => intermediate.sum == k)
            .Select(intermediate => $"{intermediate.representation} = {k}");

    private static int GetSign(this string input) => input[0] == '-' ? -1 : 1;
}