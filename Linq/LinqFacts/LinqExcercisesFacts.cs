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
    public void GetFirstCharacterThatDoesNotRepeat()
    {
        string s = "abbrac";
        Assert.Equal('r', s.GetFirstCharacterThatDoesNotRepeat());
    }

    [Fact]
    public void GetFirstCharacterThatDoesNotRepeat_EveryCharIsRepeating_ShouldThrowInvalidOperationException()
    {
        string s = "abbrra";
        Assert.Throws<InvalidOperationException>(() => s.GetFirstCharacterThatDoesNotRepeat());
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
    public void GenerateSum_InputShouldProduce3Representations()
    {
        int n = 5;
        int k = 3;
        Assert.True(LinqExercises.GenerateSum(n, k).SequenceEqual
            (new List<string>() {
                "-1 + 2 + 3 + 4 + -5 = 3",
                "1 + -2 + 3 + -4 + 5 = 3",
                "-1 + -2 + -3 + 4 + 5 = 3",
            }));
    }

    [Fact]
    public void GenerateSum_InputShouldProduceNoRepresentation()
    {
        int n = 1;
        int k = 3;
        Assert.True(LinqExercises.GenerateSum(n, k).SequenceEqual(new List<string>() { }));
    }

    [Fact]
    public void GenerateSum_NIsNegative_ShouldThrowException()
    {
        int n = -1;
        int k = 3;
        Assert.Throws<ArgumentException>(() => LinqExercises.GenerateSum(n, k).SequenceEqual(new List<string>() { }));
    }

    [Fact]
    public void GetPythagoreanTriplets_InputHasSeveralElements_OutputShouldHave2Pairs()
    {
        int[] array = { 1, 2, 6, 4, 8, 9, 11, 3, 17, 15, 5 };
        Assert.True(array.GetPythagoreanTriplets().SequenceEqual(new List<(int, int, int)>() { (4, 3, 5), (8, 17, 15) }));
    }

    [Fact]
    public void GetPythagoreanTriplets_InputHasInsufficientElements_ShouldThrowException()
    {
        int[] array = { 1, 2 };
        Assert.Throws<ArgumentException>(() => array.GetPythagoreanTriplets().ToList());
    }

    [Fact]
    public void GetPythagoreanTriplets_InputIsNull_ShouldThrowException()
    {
        int[] array = null;
        Assert.Throws<NullReferenceException>(() => array.GetPythagoreanTriplets().ToList());
    }

    [Fact]
    public void GetPythagoreanTriplets_InputHasNoPythagoreanTriplets_ShouldReturnEmptyEnumerable()
    {
        int[] array = { 1, 2, 3, 4, 10};
        Assert.True(array.GetPythagoreanTriplets().SequenceEqual(new List<(int, int, int)>() { }));
    }

    [Fact]
    public void FilterProductsContainAnyFeature_InputHasSeveralElements_OutputShouldBeAnIEnumerableWith2Elements()
    {
        var firstFeature = new Feature(1);
        var secondFeature = new Feature(8);
        List<Feature> features1 = new() { firstFeature, new Feature(2), new Feature(3) };
        List<Feature> features2 = new() { new Feature(9), new Feature(12), new Feature(53) };
        List<Feature> features3 = new() { secondFeature, new Feature(54), new Feature(99) };

        SecondProduct product1 = new("product1", features1);
        SecondProduct product2 = new("product2", features2);
        SecondProduct product3 = new("product3", features3);

        List<Feature> featuresToCheck = new() { firstFeature, new Feature(7), secondFeature };
        List<SecondProduct> products = new() { product1, product2, product3 };

        Assert.True(
            products.FilterProductsContainAnyFeature(featuresToCheck)
                .SequenceEqual(new List<SecondProduct>() { product1, product3 }));
    }

    [Fact]
    public void FilterProductsContainAnyFeature_InputIsNull_ShouldThrowException()
    {
        List<Feature> features = new() { new Feature(1), new Feature(2) };
        List<SecondProduct> products = null;
        Assert.Throws<ArgumentNullException>(() => products.FilterProductsContainAnyFeature(features).ToList());
    }

    [Fact]
    public void FilterProductsContainAllFeatures_InputHasSeveralElements_OutputShouldBeAnIEnumerableWith1Element()
    {
        var firstFeature = new Feature(1);
        var secondFeature = new Feature(8);
        List<Feature> features1 = new() { firstFeature, secondFeature, new Feature(3) };
        List<Feature> features2 = new() { new Feature(9), firstFeature, new Feature(53) };
        List<Feature> features3 = new() { secondFeature, new Feature(54), new Feature(99) };

        SecondProduct product1 = new("product1", features1);
        SecondProduct product2 = new("product2", features2);
        SecondProduct product3 = new("product3", features3);

        List<Feature> featuresToCheck = new() { firstFeature, secondFeature };
        List<SecondProduct> products = new() { product1, product2, product3 };

        Assert.True(
            products.FilterProductsContainAllFeatures(featuresToCheck)
                .SequenceEqual(new List<SecondProduct> { product1 }));
    }

    [Fact]
    public void FilterProductsContainAllFeatures_InputIsNull_ShouldThrowException()
    {
        List<Feature> features = new() { new Feature(1), new Feature(2) };
        List<SecondProduct> products = null;
        Assert.Throws<ArgumentNullException>(() => products.FilterProductsContainAllFeatures(features).ToList());
    }
}