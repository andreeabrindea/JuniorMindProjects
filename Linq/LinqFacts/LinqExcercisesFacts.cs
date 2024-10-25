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
        Assert.Throws<NullReferenceException>(() => s.GetCharacterWithMaximumNoOfOccurrences());
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
}