namespace Linq;

public static class LinqExercises
{
    public static (int Consonants, int Vowels) GetNoOfConsonantsAndVowels(this string input)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        return (input.Count(character => !"aeiou".Contains(character) && char.IsLetter(character)),
            input.Count(character => "aeiou".Contains(character) && char.IsLetter(character)));
    }

    public static char GetFirstCharacterThatDoesNotRepeat(this string input)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        return input.ToLookup(p => p).First(p => p.Count() == 1).Key;
    }

    public static int ConvertStringToInt(this string input)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        const int ten = 10;
        int sign = GetSign(input);
        if ("-+".Any(prefix => input.StartsWith(prefix)))
        {
            input = input[1..];
        }

        var result = input.Aggregate(0, (accumulate, character) =>
        {
            if (character is < '0' or > '9')
            {
                throw new FormatException(nameof(input));
            }

            return accumulate * ten + (character - '0');
        });
        return sign * result;
    }

    public static int GetCharacterWithMaximumNoOfOccurrences(this string input)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        return input.GroupBy(p => p).MaxBy(p => p.Count())!.Key;
    }

    public static IEnumerable<string> GetPalindromes(this string input)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(input));
        return Enumerable
            .Range(1, input.Length)
            .SelectMany(length => Enumerable.Range(0, input.Length - length + 1)
                .Select(a => input.Substring(a, length)))
            .Where(b => b.SequenceEqual(b.Reverse()) && b.Length > 1)
            .Distinct();
    }

    public static IEnumerable<string> GenerateSum(int n, int k)
    {
        if (n < 1)
        {
            throw new ArgumentException("{0} cannot be smaller than 1.", nameof(n));
        }

        return Enumerable.Range(0, 1 << n)
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
    }

    public static IEnumerable<(int, int, int)> GetPythagoreanTriplets(this int[] array)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(array));
        if (array.Length < 3)
        {
            throw new ArgumentException("{0} has insufficient elements.", nameof(array));
        }

        const int skipPositionsForFirstIterator = 1;
        const int skipPositionsForSecondIterator = 2;
        return array
            .SelectMany((a, i) => array.Skip(i + skipPositionsForFirstIterator)
                .SelectMany((b, j) => array.Skip(i + j + skipPositionsForSecondIterator)
                    .Where(c => ArePythagoreanTriplets(a, b, c))
                    .Select(c => (a, b, c))));
    }

    public static IEnumerable<SecondProduct> FilterProductsContainAnyFeature(this IEnumerable<SecondProduct> products, IEnumerable<Feature> features)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(products));
        ArgumentException.ThrowIfNullOrEmpty(nameof(features));
        return products.Where(product => product.Features.Any(features.Contains));
    }

    public static IEnumerable<SecondProduct> FilterProductsContainAllFeatures(this IEnumerable<SecondProduct> products, IEnumerable<Feature> features)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(products));
        ArgumentException.ThrowIfNullOrEmpty(nameof(features));
        return products.Where(product => features.All(product.Features.Contains));
    }

    public static IEnumerable<SecondProduct> FilterProductsThatDoNotContainAnyFeature(this IEnumerable<SecondProduct> products, IEnumerable<Feature> features)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(products));
        ArgumentException.ThrowIfNullOrEmpty(nameof(features));
        return products.Where(product => features.All(feature => !product.Features.Contains(feature)));
    }

    private static int GetSign(this string input) => input[0] == '-' ? -1 : 1;

    private static bool ArePythagoreanTriplets(int a, int b, int c)
    {
        const int two = 2;
        return (int)(Math.Pow(a, two) + Math.Pow(b, two)) == (int)Math.Pow(c, two) ||
               (int)(Math.Pow(a, two) + Math.Pow(c, two)) == (int)Math.Pow(b, two) ||
               (int)(Math.Pow(c, two) + Math.Pow(b, two)) == (int)Math.Pow(a, two);
    }
}