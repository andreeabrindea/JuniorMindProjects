using System.Data;

namespace Linq;

public static class LinqExercises
{
    public static (int Consonants, int Vowels) GetNoOfConsonantsAndVowels(this string input)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(input);
        int vowels = input.Count(p => "aeiou".Contains(p));
        int consonants = input.Length - vowels - input.Count(c => !char.IsLetter(c));
        return (consonants, vowels);
    }

    public static char GetFirstCharacterThatDoesNotRepeat(this string input)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(input);
        return input.GroupBy(p => p).FirstOrDefault(p => p.Count() == 1)!.Key;
    }

    public static int ConvertStringToInt(this string input)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(input);
        if (input.Length == 1)
        {
            return char.IsDigit(input[0]) ? input[0] - '0' : throw new FormatException();
        }

        int sign = 1;
        if ("-+".Any(sign => input.Contains(sign)))
        {
            sign = input[0] == '-' ? -1 : 1;
            input = input[1..];
        }

        var result = input.Aggregate(0, (accumulate, c) =>
        {
            if (c is > '9' || c < '0')
            {
                throw new FormatException();
            }

            return accumulate * 10 + (c - '0');
        });

        return result * sign;
    }

    public static char GetCharacterWithMaximumNoOfOccurrences(this string input)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(input);
        return input.GroupBy(p => p).MaxBy(p => p.Count()).Key;
    }

    public static IEnumerable<string> GetPalindromes(this string input)
    {
        const int minimumLength = 2;
        return Enumerable.Range(minimumLength, input.Length)
            .SelectMany(length => Enumerable.Range(0, input.Length - length + 1).Select(start => input.Substring(start, length))
                .Where(substring => substring.SequenceEqual(substring.Reverse()))
                .Distinct());
    }

    public static IEnumerable<IEnumerable<int>> GenerateSubsequencesWithSumLessThanK(this int[] nums, int k)
    {
        ArgumentNullException.ThrowIfNull(nums);
        if (k < 1)
        {
            throw new InvalidOperationException(nameof(k));
        }

        const int minimumLength = 1;
        return Enumerable.Range(minimumLength, nums.Length).SelectMany(length =>
                Enumerable.Range(0, nums.Length - length + 1).Select(start => nums.Skip(start).Take(length)))
            .Where(sequence => sequence.Sum() <= k);
    }

    public static List<string> GenerateSum(this int n, int k)
    {
        if (n < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(n));
        }

        int[] nums = Enumerable.Range(1, n).ToArray();
        List<string> expressions = new List<string>();
        GenerateCombinations(nums, k, 0, 0, "", expressions);
        return expressions;
    }

    public static IEnumerable<(int, int, int)> GetPythagoreanTriplets(this int[] nums)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(nums));
        if (nums.Length < 3)
        {
            throw new ArgumentException("{0} has insufficient elements.", nameof(nums));
        }

        const int skipPositionsForFirstIterator = 1;
        const int skipPositionsForSecondIterator = 2;

        return nums.SelectMany((a, i) => nums.Skip(i + skipPositionsForFirstIterator)
            .SelectMany((b, j) => nums.Skip(i + j + skipPositionsForSecondIterator)
                .Where(c => ArePythagoreanTriplets(a, b, c))
            .Select(c => (a, b, c))));
    }

    public static IEnumerable<ProductFromExercise10> FilterProductsContainAnyFeature(
        this IEnumerable<ProductFromExercise10> products, IEnumerable<Feature> features)
    {
        ArgumentNullException.ThrowIfNull(products);
        ArgumentNullException.ThrowIfNull(features);
        HashSet<Feature> featuresSet = new(features);
        return products.Where(product => featuresSet.Any(feature => product.Features.Contains(feature)));
    }

    public static IEnumerable<ProductFromExercise10> FilterProductsContainAllFeatures(
        this IEnumerable<ProductFromExercise10> products, IEnumerable<Feature> features)
    {
        ArgumentNullException.ThrowIfNull(products);
        ArgumentNullException.ThrowIfNull(features);
        HashSet<Feature> featuresSet = new(features);
        return products.Where(product => featuresSet.All(feature => product.Features.Contains(feature)));
    }

    public static IEnumerable<ProductFromExercise10> FilterProductsThatDoNotContainAnyFeature(
        this IEnumerable<ProductFromExercise10> products, IEnumerable<Feature> features)
    {
        ArgumentNullException.ThrowIfNull(products);
        ArgumentNullException.ThrowIfNull(features);
        HashSet<Feature> featuresSet = new(features);
        return products.Where(product => featuresSet.All(feature => !product.Features.Contains(feature)));
    }

    public static IEnumerable<ProductFromExercise11> ConcatenateByProductName(this List<ProductFromExercise11> firstListOfProducts, List<ProductFromExercise11> secondListOfProducts)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(firstListOfProducts));
        ArgumentException.ThrowIfNullOrEmpty(nameof(secondListOfProducts));
        return firstListOfProducts.Concat(secondListOfProducts)
            .GroupBy(product => product.Name)
            .Select(group => new ProductFromExercise11(group.Key, group.Sum(q => q.Quantity)));
    }

    public static IEnumerable<TestResults?> MergeEntriesWithSameFamilyId(this List<TestResults> testResults)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(testResults));
        return testResults.GroupBy(t => t.FamilyId).Select(t => t.MaxBy(test => test.Score));
    }

    public static IEnumerable<string> GetMostUsedWords(this string input, int numberOfWords)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(input);
        char[] delimiterChars = { ' ', '.', ',', '\t' };
        return input
            .ToLower()
            .Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries)
            .GroupBy(word => word)
            .OrderByDescending(p => p.Count())
            .Select(p => p.Key)
            .Take(numberOfWords);
    }

    public static bool IsValidSudokuBoard(this int[][] board)
    {
        ArgumentNullException.ThrowIfNull(board);
        const int columnRowsLength = 9;

        if (!HasSudokuValidLength(board, columnRowsLength))
        {
            return false;
        }

        return Enumerable.Range(0, columnRowsLength).All(
            index =>
            {
                if (HasDuplicates(board[index]) || !HasAllElementsDigits(board[index]))
                {
                    return false;
                }

                var column = Enumerable.Range(0, columnRowsLength).Select(
                    rowIndex => board[index][rowIndex]);
                {
                    if (HasDuplicates(column) || !HasAllElementsDigits(column))
                    {
                        return false;
                    }
                }

                var subgridElements = Enumerable.Range(0, 9).Select(i => board[index / 3 * 3 + i / 3][index % 3 * 3 + i % 3]);
                return !HasDuplicates(subgridElements) && HasAllElementsDigits(subgridElements);
            });
    }

    public static double SolvePostfixNotationExpression(this string input)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(input);
        Dictionary<string, Func<double, double, double>> operators = new()
        {
            { "-", (a, b) => a - b },
            { "+", (a, b) => a + b },
            { "*", (a, b) => a * b },
            { "/", (a, b) => a / b }
        };

        Stack<double> stack = new();
        input.Split(' ')
            .ToList()
            .ForEach(element =>
            {
                if (double.TryParse(element, out double number))
                {
                    stack.Push(number);
                }
                else if (operators.ContainsKey(element))
                {
                    if (stack.Count < 2)
                    {
                        throw new InvalidExpressionException("Insufficient operands for the operation.");
                    }

                    double a = stack.Pop();
                    double b = stack.Pop();
                    stack.Push(operators[element](b, a));
                }
                else
                {
                    throw new InvalidExpressionException("Invalid token.");
                }
            });

        return stack.Count == 1 ? stack.Pop() : 0;
    }

    private static bool HasSudokuValidLength(this int[][] board, int rowAndColumnLength) =>
        board.Length == rowAndColumnLength && board.All(row => row.Length == rowAndColumnLength);

    private static bool HasDuplicates(IEnumerable<int> array)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(array));
        return array.Where(num => num != 0).GroupBy(num => num).Any(g => g.Count() > 1);
    }

    private static bool HasAllElementsDigits(IEnumerable<int> array)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(array));
        const int biggestDigit = 9;
        const int smallestDigit = 0;
        return array.All(r => r is >= smallestDigit and <= biggestDigit);
    }

    private static bool ArePythagoreanTriplets(int a, int b, int c)
    {
        return a * a + b * b == c * c || a * a + c * c == b * b || b * b + c * c == a * a;
    }

    private static void GenerateCombinations(int[] nums, int k, int currentIndex, int currentSum, string currentExpression, List<string> expressions)
    {
        if (currentIndex == nums.Length)
        {
            if (currentSum == k)
            {
                expressions.Add(currentExpression.TrimStart('+') + "=" + k);
            }

            return;
        }

        GenerateCombinations(
            nums,
            k,
            currentIndex + 1,
            currentSum + nums[currentIndex],
            currentExpression + "+" + nums[currentIndex],
            expressions);

        GenerateCombinations(
            nums,
            k,
            currentIndex + 1,
            currentSum - nums[currentIndex],
            currentExpression + "-" + nums[currentIndex],
            expressions);
    }
}
