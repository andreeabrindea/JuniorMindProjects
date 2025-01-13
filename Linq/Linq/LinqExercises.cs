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
