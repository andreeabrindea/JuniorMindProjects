using Xunit;

namespace Linq.Facts;

public class LinqFacts
{
    [Fact]
    public void All_ListOfIntegerEven_ShouldReturnFalse()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
        Assert.False(numbers.All(i => i % 2 == 0));
    }

    [Fact]
    public void All_ListOfIntegersUnder10_ShouldReturnTrue()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
        Assert.False(numbers.All(i => i % 2 == 0));
    }

    [Fact]
    public void All_ListOfStringsStartsWithA_ShouldReturnTrue()
    {
        List<string> words = new List<string> { "abc", "air", "apple" };
        Assert.True(words.All(s => s.StartsWith('a')));
    }

    [Fact]
    public void All_ListOfStringsStartsWithA_ShouldReturnFalse()
    {
        List<string> words = new List<string> { "abc", "air", "apple", "hello" };
        Assert.False(words.All(s => s.StartsWith('a')));
    }

    [Fact]
    public void Any_ListOfIntegerEven_ShouldReturnTrue()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
        Assert.True(numbers.Any(i => i % 2 == 0));
    }

    [Fact]
    public void Any_ListOfIntegerEven_ShouldReturnFalse()
    {
        List<int> numbers = new List<int> { 1, 3, 5, 7 };
        Assert.False(numbers.Any(i => i % 2 == 0));
    }

    [Fact]
    public void Any_ListOfStringsStartsWithA_ShouldReturnTrue()
    {
        List<string> words = new List<string> { "abc", "air", "apple", "hello" };
        Assert.True(words.Any(s => s.StartsWith('a')));
    }

    [Fact]
    public void Any_ListOfStringsStartsWithA_ShouldReturnFalse()
    {
        List<string> words = new List<string> { "bcd", "cdef", "hello" };
        Assert.False(words.Any(s => s.StartsWith('a')));
    }

    [Fact]
    public void First_ElementThatStartsWithA()
    {
        List<string> words = new List<string> { "bcd", "air", "hello" };
        Assert.Equal("air",words.First(s => s.StartsWith('a')));
    }

    [Fact]
    public void First_ElementThatStartsWithA_WhenThereIsNoElement()
    {
        List<string> words = new List<string> { "bcd", "cdef", "hello" };
        Assert.Throws<InvalidOperationException>(() => words.First(s => s.StartsWith('a')));
    }

    [Fact]
    public void First_EvenElement_WhenThereIsNoElement()
    {
        List<int> numbers = new List<int> {1, 7, 5, 9, 21, 73};
        Assert.Throws<InvalidOperationException>(() => numbers.First(i => i % 2 == 0));
    }

    [Fact]
    public void First_EvenElement()
    {
        List<int> numbers = new List<int> {1, 7, 8, 9, 20, 2, 4};
        Assert.Equal(8, numbers.First(i => i % 2 == 0));
    }
}