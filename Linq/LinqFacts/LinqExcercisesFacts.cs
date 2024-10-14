using Xunit;

namespace Linq.Facts;

public class LinqExcercisesFacts
{
    [Fact]
    public void GetNoOfConsonantsAndVowels_FromStringWithSpaceAndSpecialCharacter()
    {
        string s = "hello there!";
        Assert.Equal((6, 4), LinqExercises.GetNoOfConsonantsAndVowels(s));
    }

    [Fact]
    public void GetNoOfConsonantsAndVowels_FromStringWithoutLetters()
    {
        string s = "123456789";
        Assert.Equal((0, 0), LinqExercises.GetNoOfConsonantsAndVowels(s));
    }

    [Fact]
    public void GetFirstCharacterThatDoesNotRepeat()
    {
        string s = "abbrac";
        Assert.Equal('r', LinqExercises.GetFirstCharacterThatDoesNotRepeat(s));
    }

    [Fact]
    public void GetFirstCharacterThatDoesNotRepeat_EveryCharIsRepeating_ShouldThrowInvalidOperationException()
    {
        string s = "abbrra";
        Assert.Throws<InvalidOperationException>(() => LinqExercises.GetFirstCharacterThatDoesNotRepeat(s));
    }

    [Fact]
    public void ConvertsStringToInt_StringIsNumber()
    {
        string s = "100";
        Assert.Equal(100, LinqExercises.ConvertStringToInt(s));
    }

    [Fact]
    public void ConvertsStringToInt_StringIsNotNumber()
    {
        string s = "abc";
        Assert.Throws<FormatException>(() => LinqExercises.ConvertStringToInt(s));
    }
}