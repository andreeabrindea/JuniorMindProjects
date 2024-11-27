namespace Linq;

public static class LinqExercises
{
    public static (int Consonants, int Vowels) GetNoOfConsonantsAndVowels(this string input)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        var result = input.ToLower()
            .Where(char.IsLetter)
            .GroupBy(c => "aeiou".Contains(c) ? "Vowels" : "Consonants")
            .Select(g => new { Type = g.Key, Count = g.Count() })
            .ToDictionary(x => x.Type, x => x.Count);

        int vowelCount = result.TryGetValue("Vowels", out var value) ? value : 0;
        int consonantCount = result.TryGetValue("Consonants", out var value1) ? value1 : 0;

        return (consonantCount, vowelCount);
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
        const int minimumLength = 2;
        return Enumerable
            .Range(minimumLength, input.Length)
            .SelectMany(length => Enumerable.Range(0, input.Length - length + 1)
                .Select(a => input.Substring(a, length)))
            .Where(b => b.SequenceEqual(b.Reverse()))
            .Distinct();
    }

    public static IEnumerable<string> GenerateSum(int n, int k)
    {
        if (n < 1)
        {
            throw new ArgumentException("{0} cannot be smaller than 1.", nameof(n));
        }

        const int noOfPossibleSignsPerElement = 2;
        return Enumerable.Range(0, (int)Math.Pow(noOfPossibleSignsPerElement, n))
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

    public static IEnumerable<ProductFromExercise10> FilterProductsContainAnyFeature(this IEnumerable<ProductFromExercise10> products, IEnumerable<Feature> features)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(products));
        ArgumentException.ThrowIfNullOrEmpty(nameof(features));
        HashSet<Feature> featuresSet = new HashSet<Feature>(features);
        return products.Where(product => product.Features.Any(featuresSet.Contains));
    }

    public static IEnumerable<ProductFromExercise10> FilterProductsContainAllFeatures(this IEnumerable<ProductFromExercise10> products, IEnumerable<Feature> features)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(products));
        ArgumentException.ThrowIfNullOrEmpty(nameof(features));
        HashSet<Feature> featuresSet = new HashSet<Feature>(features);
        return products.Where(product => featuresSet.IsSubsetOf(product.Features));
    }

    public static IEnumerable<ProductFromExercise10> FilterProductsThatDoNotContainAnyFeature(this IEnumerable<ProductFromExercise10> products, IEnumerable<Feature> features)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(products));
        ArgumentException.ThrowIfNullOrEmpty(nameof(features));
        HashSet<Feature> featuresSet = new HashSet<Feature>(features);
        return products.Where(product => product.Features.All(feature => !featuresSet.Contains(feature)));
    }

    public static IEnumerable<ProductFromExercise11> ConcatenateByProductName(this List<ProductFromExercise11> firstListOfProducts, List<ProductFromExercise11> secondListOfProducts)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(firstListOfProducts));
        ArgumentException.ThrowIfNullOrEmpty(nameof(secondListOfProducts));
        return firstListOfProducts.Concat(secondListOfProducts)
            .GroupBy(product => product.Name)
            .Select(group => new ProductFromExercise11(group.Key, group.Sum(q => q.Quantity)));
    }

    public static List<TestResults> MergeEntriesWithSameFamilyId(this List<TestResults> testResults)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(testResults));
        return testResults.GroupBy(t => t.FamilyId).Select(t => t.MaxBy(test => test.Score)).ToList();
    }

    public static IEnumerable<string> GetMostUsedWords(this string text, int n)
    {
        ArgumentException.ThrowIfNullOrEmpty(text);
        char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
        return text.ToLower()
            .Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries)
            .GroupBy(word => word)
            .OrderByDescending(entry => entry.Count())
            .Select(entry => entry.Key)
            .Take(n);
    }

    public static bool IsValidSudokuBoard(this int[][] board)
    {
        bool areRowsValid = Enumerable.Range(0, 9).All(row => !HasDuplicates(board[row]) && HasAllElementsDigits(board[row]));

        bool areColumnsValid = Enumerable.Range(0, 9)
            .All(column =>
            {
                var columnElements = Enumerable.Range(0, 9).Select(row => board[row][column]);
                return !HasDuplicates(columnElements) &&
                       HasAllElementsDigits(columnElements);
            });

        bool areSubgridsValid = Enumerable.Range(0, 9)
            .All(blockIndex =>
            {
                var subgridElements = Enumerable.Range(0, 9).Select(i => board[blockIndex / 3 * 3 + i / 3][blockIndex % 3 * 3 + i % 3]);
                return !HasDuplicates(subgridElements) &&
                       HasAllElementsDigits(subgridElements);
            });

        return areRowsValid && areColumnsValid && areSubgridsValid;
    }

    private static bool HasDuplicates(IEnumerable<int> array)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(array));
        return array.Where(num => num != 0).GroupBy(num => num).Any(g => g.Count() > 1);
    }

    private static bool HasAllElementsDigits(IEnumerable<int> array)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(array));
        return array.All(r => r.IsDigit());
    }

    private static bool IsDigit(this int number)
    {
        const int lowerBound = 0;
        const int higherBound = 10;
        return number is > lowerBound and < higherBound;
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