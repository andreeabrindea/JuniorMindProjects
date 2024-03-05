using Xunit; 

namespace Json.Facts;

public class RemainingTextFacts 
{
    [Fact]
    public void InputIsEmpty()
    {
        string input = string.Empty;
        var remainingText = new StringView(input);
        remainingText.IncrementIndex();
        Assert.Equal(string.Empty, remainingText.RemoveSubstringFromIndex());
    }

    [Fact]
    public void InputIsNull()
    {
        var remainingText = new StringView(null);
        Assert.Null(remainingText.RemoveSubstringFromIndex());
    }

    [Fact]
    public void RemoveSubstringFromIndexZero()
    {
        string input = "hello";
        var remainingText = new StringView(input);
        Assert.Equal("hello", remainingText.RemoveSubstringFromIndex());
        
    }
    
    [Fact]
    public void RemoveSubstringFromIndexOne()
    {
        string input = "hello";
        var remainingText = new StringView(input);
        remainingText.IncrementIndex();
        Assert.Equal("ello", remainingText.RemoveSubstringFromIndex());
    }

    [Fact]
    public void RemoveSubstringFromIndexThree()
    {
        string input = "hello there";
        var remainingText = new StringView(input);
        remainingText.IncrementIndex();
        remainingText.IncrementIndex();
        remainingText.IncrementIndex();
        
        Assert.Equal("lo there", remainingText.RemoveSubstringFromIndex());
    }
}