using System.Data;
using Xunit;

namespace Linq.Facts;

public class LinqExcercisesFacts
{
    [Fact]
    public void GetNoOfConsonantsAndVowels_FromStringWithSpaceAndSpecialCharacter()
    {
        string s = "hello there!";
        Assert.Equal((6, 4), s.GetNoOfConsonantsAndVowels());
    }

    [Fact]
    public void GetNoOfConsonantsAndVowels_FromStringWithoutLetters()
    {
        string s = "123456789";
        Assert.Equal((0, 0), s.GetNoOfConsonantsAndVowels());
    }

    [Fact]
    public void GetFirstCharactert()
    {
        string s = "abrc";
        Assert.Equal('a', s.GetFirstCharacterThatDoesNotRepeat());
    }

    [Fact]
    public void GetFirstCharacterThatDoesNotRepeat()
    {
        string s = "abbrac";
        Assert.Equal('r', s.GetFirstCharacterThatDoesNotRepeat());
    }

    [Fact]
    public void GetFirstCharacterThatDoesNotRepeat_EveryCharIsRepeating_ShouldThrowException()
    {
        string s = "abbrra";
        Assert.Throws<NullReferenceException>(() => s.GetFirstCharacterThatDoesNotRepeat());
    }

    [Fact]
    public void ConvertsStringToInt_StringIsNumber()
    {
        string s = "100";
        Assert.Equal(100, s.ConvertStringToInt());
    }

    [Fact]
    public void ConvertsStringToInt_StringIsNegativeNumber()
    {
        string s = "-100";
        Assert.Equal(-100, s.ConvertStringToInt());
    }

    [Fact]
    public void ConvertsStringToInt_StringIsPositiveNumberWithSign()
    {
        string s = "+100";
        Assert.Equal(+100, s.ConvertStringToInt());
    }

    [Fact]
    public void ConvertsStringToInt_StringIsNotNumber()
    {
        string s = "abc";
        Assert.Throws<FormatException>(() => s.ConvertStringToInt());
    }

    [Fact]
    public void GetCharacterWithMaximumNoOfOccurrences_InputHasADominantCharacter_ShouldReturnDominantCharacter()
    {
        string s = "aaabbcdefgh";
        Assert.Equal('a', s.GetCharacterWithMaximumNoOfOccurrences());
    }

    [Fact]
    public void GetCharacterWithMaximumNoOfOccurrences_InputDoesNotHaveADominantCharacter_ShouldReturnFirstCharacter()
    {
        string s = "abc";
        Assert.Equal('a', s.GetCharacterWithMaximumNoOfOccurrences());
    }

    [Fact]
    public void GetCharacterWithMaximumNoOfOccurrences_InputIsEmpty_ShouldThrowException()
    {
        string s = "";
        Assert.Throws<ArgumentException>(() => s.GetCharacterWithMaximumNoOfOccurrences());
    }

    [Fact]
    public void GetPalindromes_NonEmptyString()
    {
        string s = "aabaac";
        Assert.True(s.GetPalindromes().SequenceEqual(new List<string>() { "aa", "aba", "aabaa" }));
    }

    [Fact]
    public void GetPalindromes_EmptyInput()
    {
        string s = string.Empty;
        Assert.True(s.GetPalindromes().SequenceEqual(new List<string>() { }));
    }

    [Fact]
    public void GenerateSubsequencesWithSumLessThanK_InputIsNull_ShoulwThrowException()
    {
        int[] nums = null;
        int k = 2;
        Assert.Throws<ArgumentNullException>(() => nums.GenerateSubsequencesWithSumLessThanK(k));
    }

    [Fact]
    public void GenerateSubsequencesWithSumLessThanK_KIsLessThan0_ShouldThrowException()
    {
        int[] nums = { 1, 2, 3, 4 };
        int k = -2;
        Assert.Throws<InvalidOperationException>(() => nums.GenerateSubsequencesWithSumLessThanK(k));
    }

    [Fact]
    public void GenerateSubsequencesWithSumLessThanK_AllElementsSumUpToK_ShouldReturnExpected()
    {
        int[] nums = { 1, 2, 3 };
        int k = 6;
        Assert.Equal(new[] { new[] { 1 }, new[] { 2 }, new[] { 3 }, new[] { 1, 2 }, new[] { 2, 3 }, new[] { 1, 2, 3 } }, nums.GenerateSubsequencesWithSumLessThanK(k));
    }

    [Fact]
    public void GenerateSubsequencesWithSumLessThanK_NotAllElementsSumUpToK_ShouldReturnExpected()
    {
        int[] nums = { 1, 2, 3, 4, 5, 6 };
        int k = 6;
        Assert.Equal(new[] { new[] { 1 }, new[] { 2 }, new[] { 3 }, new[] { 4 }, new[] { 5 }, new[] { 6 }, new[] { 1, 2 }, new[] { 2, 3 }, new[] { 1, 2, 3 } }, nums.GenerateSubsequencesWithSumLessThanK(k));
    }

    [Fact]
    public void GenerateSum_TargetIsNegative_ShouldReturnExpected()
    {
        int n = 2;
        int k = -1;
        Assert.Equal(
            LinqExercises.GenerateSum(n, k),
            new List<string>()
            {
               "1-2=-1",
            });
    }

    [Fact]
    public void GenerateSum_InputShouldProduce3Representations()
    {
        int n = 5;
        int k = 3;
        Assert.Equal(
            LinqExercises.GenerateSum(n, k),
            new List<string>()
            {
                "1-2+3-4+5=3",
                "-1+2+3+4-5=3",
                "-1-2-3+4+5=3",
            });
    }

    [Fact]
    public void GenerateSum_InputShouldProduceNoRepresentation()
    {
        int n = 1;
        int k = 3;
        Assert.Equal(LinqExercises.GenerateSum(n, k), new List<string>() { });
    }

    [Fact]
    public void GenerateSum_NIsNegative_ShouldThrowException()
    {
        int n = -1;
        int k = 3;
        Assert.Throws<ArgumentOutOfRangeException>(() => LinqExercises.GenerateSum(n, k).SequenceEqual(new List<string>() { }));
    }

    //
    // [Fact]
    // public void GetPythagoreanTriplets_InputHasSeveralElements_OutputShouldHave2Pairs()
    // {
    //     int[] array = { 1, 2, 6, 4, 8, 9, 11, 3, 17, 15, 5 };
    //     Assert.True(array.GetPythagoreanTriplets().SequenceEqual(new List<(int, int, int)>() { (4, 3, 5), (8, 17, 15) }));
    // }
    //
    // [Fact]
    // public void GetPythagoreanTriplets_InputHasInsufficientElements_ShouldThrowException()
    // {
    //     int[] array = { 1, 2 };
    //     Assert.Throws<ArgumentException>(() => array.GetPythagoreanTriplets().ToList());
    // }
    //
    // [Fact]
    // public void GetPythagoreanTriplets_InputIsNull_ShouldThrowException()
    // {
    //     int[] array = null;
    //     Assert.Throws<NullReferenceException>(() => array.GetPythagoreanTriplets().ToList());
    // }
    //
    // [Fact]
    // public void GetPythagoreanTriplets_InputHasNoPythagoreanTriplets_ShouldReturnEmptyEnumerable()
    // {
    //     int[] array = { 1, 2, 3, 4, 10};
    //     Assert.True(array.GetPythagoreanTriplets().SequenceEqual(new List<(int, int, int)>() { }));
    // }
    //
    // [Fact]
    // public void FilterProductsContainAnyFeature_InputHasSeveralElements_OutputShouldBeAnIEnumerableWith2Elements()
    // {
    //     var firstFeature = new Feature(1);
    //     var secondFeature = new Feature(8);
    //     List<Feature> features1 = new() { firstFeature, new Feature(2), new Feature(3) };
    //     List<Feature> features2 = new() { new Feature(9), new Feature(12), new Feature(53) };
    //     List<Feature> features3 = new() { secondFeature, new Feature(54), new Feature(99) };
    //
    //     ProductFromExercise10 product1 = new("product1", features1);
    //     ProductFromExercise10 product2 = new("product2", features2);
    //     ProductFromExercise10 product3 = new("product3", features3);
    //
    //     List<Feature> featuresToCheck = new() { firstFeature, new Feature(7), secondFeature };
    //     List<ProductFromExercise10> products = new() { product1, product2, product3 };
    //
    //     Assert.True(
    //         products.FilterProductsContainAnyFeature(featuresToCheck)
    //             .SequenceEqual(new List<ProductFromExercise10>() { product1, product3 }));
    // }
    //
    // [Fact]
    // public void FilterProductsContainAnyFeature_InputIsNull_ShouldThrowException()
    // {
    //     List<Feature> features = new() { new Feature(1), new Feature(2) };
    //     List<ProductFromExercise10> products = null;
    //     Assert.Throws<ArgumentNullException>(() => products.FilterProductsContainAnyFeature(features).ToList());
    // }
    //
    // [Fact]
    // public void FilterProductsContainAllFeatures_InputHasSeveralElements_OutputShouldBeAnIEnumerableWith1Element()
    // {
    //     var firstFeature = new Feature(1);
    //     var secondFeature = new Feature(8);
    //     List<Feature> features1 = new() { firstFeature, secondFeature, new Feature(3) };
    //     List<Feature> features2 = new() { new Feature(9), firstFeature, new Feature(53) };
    //     List<Feature> features3 = new() { secondFeature, new Feature(54), new Feature(99) };
    //
    //     ProductFromExercise10 product1 = new("product1", features1);
    //     ProductFromExercise10 product2 = new("product2", features2);
    //     ProductFromExercise10 product3 = new("product3", features3);
    //
    //     List<Feature> featuresToCheck = new() { firstFeature, secondFeature };
    //     List<ProductFromExercise10> products = new() { product1, product2, product3 };
    //
    //     Assert.True(
    //         products.FilterProductsContainAllFeatures(featuresToCheck)
    //             .SequenceEqual(new List<ProductFromExercise10> { product1 }));
    // }
    //
    // [Fact]
    // public void FilterProductsContainAllFeatures_InputIsNull_ShouldThrowException()
    // {
    //     List<Feature> features = new() { new Feature(1), new Feature(2) };
    //     List<ProductFromExercise10> products = null;
    //     Assert.Throws<ArgumentNullException>(() => products.FilterProductsContainAllFeatures(features).ToList());
    // }
    //
    // [Fact]
    // public void FilterProductsDoNotContainAnyFeature_InputHasSeveralElements_OutputShouldBeAnIEnumerableWith1Element()
    // {
    //     var firstFeature = new Feature(1);
    //     var secondFeature = new Feature(8);
    //     List<Feature> features1 = new() { firstFeature, secondFeature, new Feature(3) };
    //     List<Feature> features2 = new() { new Feature(9), new Feature(45), new Feature(53) };
    //     List<Feature> features3 = new() { secondFeature, new Feature(54), new Feature(99) };
    //
    //     ProductFromExercise10 product1 = new("product1", features1);
    //     ProductFromExercise10 product2 = new("product2", features2);
    //     ProductFromExercise10 product3 = new("product3", features3);
    //
    //     List<Feature> featuresToCheck = new() { firstFeature, secondFeature };
    //     List<ProductFromExercise10> products = new() { product1, product2, product3 };
    //
    //     Assert.True(
    //         products.FilterProductsThatDoNotContainAnyFeature(featuresToCheck)
    //             .SequenceEqual(new List<ProductFromExercise10> { product2 }));
    // }
    //
    // [Fact]
    // public void FilterProductsDoNotContainAnyFeature_InputIsNull_ShouldThrowException()
    // {
    //     List<Feature> features = new() { new Feature(1), new Feature(2) };
    //     List<ProductFromExercise10> products = null;
    //     Assert.Throws<ArgumentNullException>(() => products.FilterProductsThatDoNotContainAnyFeature(features).ToList());
    // }
    //
    // [Fact]
    // public void ConcatenateByProductName_InputHasSeveralObjectsInCommon_OutputShouldHave4Elements()
    // {
    //     ProductFromExercise11 book = new("book", 10);
    //     ProductFromExercise11 plant = new("plant", 5);
    //     ProductFromExercise11 carpet = new("carpet", 3);
    //     ProductFromExercise11 secondCarpet = new("carpet", 2);
    //     ProductFromExercise11 secondTypeOfBook = new("book", 12);
    //     ProductFromExercise11 tv = new("tv", 6);
    //
    //     List<ProductFromExercise11> firstList = new List<ProductFromExercise11>() { book, plant, carpet, secondCarpet };
    //     List<ProductFromExercise11> secondList = new List<ProductFromExercise11>() { book, carpet, secondTypeOfBook, tv };
    //     Assert.True(
    //         firstList.ConcatenateByProductName(secondList)
    //             .SequenceEqual(new List<ProductFromExercise11>() { new("book", 32),  new("plant", 5), new("carpet", 8), new("tv", 6) }));
    // }
    //
    // [Fact]
    // public void ConcatenateByProductName_FirstListIsNull_ShouldThrowException()
    // {
    //     ProductFromExercise11 book = new("book", 10);
    //     ProductFromExercise11 plant = new("plant", 5);
    //     List<ProductFromExercise11> firstList = null;
    //     List<ProductFromExercise11> secondList = new List<ProductFromExercise11>() { book, plant };
    //     Assert.Throws<ArgumentNullException>(() => firstList.ConcatenateByProductName(secondList));
    // }
    //
    // [Fact]
    // public void ConcatenateByProductName_SecondListIsNull_ShouldThrowException()
    // {
    //     ProductFromExercise11 book = new("book", 10);
    //     ProductFromExercise11 plant = new("plant", 5);
    //     List<ProductFromExercise11> secondList = null;
    //     List<ProductFromExercise11> firstList = new List<ProductFromExercise11> { book, plant };
    //     Assert.Throws<ArgumentNullException>(() => firstList.ConcatenateByProductName(secondList));
    // }
    //
    // [Fact]
    // public void MergeEntriesWithSameFamilyId_InputHas2EntriesWithSameFamilyId_OutputShouldHave2Elements()
    // {
    //     TestResults testResult1 = new("1", "Pop", 5);
    //     TestResults testResult2 = new("2", "Pop", 2);
    //     TestResults testResult3 = new("3", "Popescu", 3);
    //     List<TestResults> results = new() { testResult1, testResult2, testResult3 };
    //     Assert.True(results.MergeEntriesWithSameFamilyId().SequenceEqual(new List<TestResults> { testResult1, testResult3 }));
    // }
    //
    // [Fact]
    // public void MergeEntriesWithSameFamilyId_InputHasNoDuplicates_OutputShouldConcatenateTheInputLists()
    // {
    //     TestResults testResult1 = new("1", "Pop", 5);
    //     TestResults testResult2 = new("2", "Chis", 2);
    //     TestResults testResult3 = new("3", "Popescu", 3);
    //     List<TestResults> results = new() { testResult1, testResult2, testResult3 };
    //     Assert.True(results.MergeEntriesWithSameFamilyId().SequenceEqual(new List<TestResults> { testResult1, testResult2, testResult3 }));
    // }
    //
    // [Fact]
    // public void MergeEntriesWithSameFamilyId_InputIsNull()
    // {
    //     List<TestResults> results = null;
    //     Assert.Throws<ArgumentNullException>(() => results.MergeEntriesWithSameFamilyId());
    // }
    //
    // [Fact]
    // public void GetMostUsedWords_TextIsEmpty()
    // {
    //     string text = string.Empty;
    //     Assert.Throws<ArgumentException>(() => text.GetMostUsedWords(3).ToList());
    // }
    //
    // [Fact]
    // public void GetMostUsedWords_TextHasSeveralNonUniqueWords()
    // {
    //     string text = "Hello there, hello again. Hello, how it is going? Is everything ok?";
    //     Assert.True(text.GetMostUsedWords(2).SequenceEqual(new List<string> { "hello", "is" }));
    // }
    //
    // [Fact]
    // public void GetMostUsedWords_TextHasSeveralUniqueWords_ShouldTakeFirstWordsFromText()
    // {
    //     string text = "This is a random text with unique words";
    //     Assert.True(text.GetMostUsedWords(2).SequenceEqual(new List<string> { "this", "is" }));
    // }
    //
    // [Fact]
    // public void IsValidSudokuBoard_BoardIsValid_ShouldReturnTrue()
    // {
    //     int[][] sudoku =
    //     {
    //         new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
    //         new[] { 4, 5, 6, 7, 8, 9, 1, 2, 3 },
    //         new[] { 7, 8, 9, 1, 2, 3, 4, 5, 6 },
    //         new[] { 2, 3, 4, 5, 6, 7, 8, 9, 1 },
    //         new[] { 5, 6, 7, 8, 9, 1, 2, 3, 4 },
    //         new[] { 8, 9, 1, 2, 3, 4, 5, 6, 7 },
    //         new[] { 3, 4, 5, 6, 7, 8, 9, 1, 2 },
    //         new[] { 6, 7, 8, 9, 1, 2, 3, 4, 5 },
    //         new[] { 9, 1, 2, 3, 4, 5, 6, 7, 8 },
    //     };
    //
    //     Assert.True(sudoku.IsValidSudokuBoard());
    // }
    //
    // [Fact]
    // public void IsValidSudokuBoard_InputIsNull_ShouldThrowException()
    // {
    //     int[][] board = null;
    //     Assert.Throws<NullReferenceException>(() => board.IsValidSudokuBoard());
    // }
    //
    // [Fact]
    // public void IsValidSudokuBoard_RowHasDuplicates_ShouldReturnFalse()
    // {
    //     int[][] sudoku =
    //     {
    //         new[] { 1, 2, 3, 4, 5, 6, 7, 8, 2 },
    //         new[] { 4, 5, 6, 7, 8, 9, 1, 2, 3 },
    //         new[] { 7, 8, 9, 1, 2, 3, 4, 5, 6 },
    //         new[] { 2, 3, 4, 5, 6, 7, 8, 9, 1 },
    //         new[] { 5, 6, 7, 8, 9, 1, 2, 3, 4 },
    //         new[] { 8, 9, 1, 2, 3, 4, 5, 6, 7 },
    //         new[] { 3, 4, 5, 6, 7, 8, 9, 1, 2 },
    //         new[] { 6, 7, 8, 9, 1, 2, 3, 4, 5 },
    //         new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
    //     };
    //
    //     Assert.False(sudoku.IsValidSudokuBoard());
    // }
    //
    // [Fact]
    // public void IsValidSudokuBoard_ColumnHasDuplicates_ShouldReturnFalse()
    // {
    //     int[][] sudoku =
    //     {
    //         new[] { 1, 2, 3, 4, 5, 6, 7, 8, 2 },
    //         new[] { 4, 5, 6, 7, 8, 9, 1, 2, 3 },
    //         new[] { 7, 8, 9, 1, 2, 3, 4, 5, 6 },
    //         new[] { 2, 3, 4, 5, 6, 7, 8, 9, 1 },
    //         new[] { 5, 6, 7, 8, 9, 1, 2, 3, 4 },
    //         new[] { 8, 9, 1, 2, 3, 4, 5, 6, 7 },
    //         new[] { 3, 4, 5, 6, 7, 8, 9, 1, 2 },
    //         new[] { 1, 7, 8, 9, 1, 2, 3, 4, 5 },
    //         new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
    //     };
    //
    //     Assert.False(sudoku.IsValidSudokuBoard());
    // }
    //
    // [Fact]
    // public void IsValidSudokuBoard_SubgridHasDuplicates_ShouldReturnFalse()
    // {
    //     int[][] sudoku =
    //     {
    //         new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
    //         new[] { 4, 5, 1, 7, 8, 9, 1, 2, 3 },
    //         new[] { 7, 8, 9, 1, 2, 3, 4, 5, 6 },
    //         new[] { 2, 3, 4, 5, 6, 7, 8, 9, 1 },
    //         new[] { 5, 6, 7, 8, 9, 1, 2, 3, 4 },
    //         new[] { 8, 9, 1, 2, 3, 4, 5, 6, 7 },
    //         new[] { 3, 4, 5, 6, 7, 8, 9, 1, 2 },
    //         new[] { 6, 7, 8, 9, 1, 2, 3, 4, 5 },
    //         new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
    //     };
    //
    //     Assert.False(sudoku.IsValidSudokuBoard());
    // }
    //
    // [Fact]
    // public void IsValidSudokuBoard_BoardHasInvalidNumber_ShouldReturnTrue()
    // {
    //     int[][] sudoku =
    //     {
    //         new[] { 91, 2, 3, 4, 5, 6, 7, 8, 9 },
    //         new[] { 4, 5, 6, 7, 8, 9, 1, 2, 3 },
    //         new[] { 7, 8, 9, 1, 2, 3, 4, 5, 6 },
    //         new[] { 2, 3, 4, 5, 6, 7, 8, 9, 1 },
    //         new[] { 5, 6, 7, 8, 92, 1, 2, 3, 4 },
    //         new[] { 8, 9, 1, 2, 3, 4, 5, 6, 7 },
    //         new[] { 3, 4, 5, 6, 7, 8, 9, 1, 2 },
    //         new[] { 6, 7, 8, 9, 1, 2, 3, 4, 5 },
    //         new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
    //     };
    //
    //     Assert.False(sudoku.IsValidSudokuBoard());
    // }
    //
    // [Fact]
    // public void SolvePostfixNotationExpression_InputIsValid()
    // {
    //     string postfixExpression = "3 4 + 2 * 7 /";
    //     Assert.Equal(2, postfixExpression.SolvePostfixNotationExpression());
    // }
    //
    // [Fact]
    // public void SolvePostfixNotationExpression_InputIsMissingOperators()
    // {
    //     string postfixExpression = "3 4 + 2 * ";
    //     Assert.Throws<InvalidExpressionException>(() => postfixExpression.SolvePostfixNotationExpression());
    // }
    //
    // [Fact]
    // public void SolvePostfixNotationExpression_InputIsMissingOperands()
    // {
    //     string postfixExpression = "3 4 + * 7 / ";
    //     Assert.Throws<InvalidExpressionException>(() => postfixExpression.SolvePostfixNotationExpression());
    // }
}