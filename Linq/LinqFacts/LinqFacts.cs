using Xunit;

namespace Linq.Facts;

public class LinqFacts
{
    [Fact]
    public void All_ListOfIntegerEven_ShouldReturnFalse()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
        Assert.False(Linq.All(numbers, i => i % 2 == 0));
    }

    [Fact]
    public void All_ListOfIntegersUnder10_ShouldReturnTrue()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
        Assert.False(Linq.All(numbers, i => i % 2 == 0));
    }

    [Fact]
    public void All_ListOfStringsStartsWithA_ShouldReturnTrue()
    {
        List<string> words = new List<string> { "abc", "air", "apple" };
        Assert.True(Linq.All(words, s => s.StartsWith('a')));
    }

    [Fact]
    public void All_ListOfStringsStartsWithA_ShouldReturnFalse()
    {
        List<string> words = new List<string> { "abc", "air", "apple", "hello" };
        Assert.False(Linq.All(words, s => s.StartsWith('a')));
    }
}