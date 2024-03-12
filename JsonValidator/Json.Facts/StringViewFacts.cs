using Xunit; 

namespace Json.Facts;

public class StringViewFacts 
{
    [Fact]
    public void PeekCharAtIndexZero()
    {
        StringView input = new("hello");
        Assert.Equal('h', input.Peek());
    }

    [Fact]
    public void PeekCharAtLastIndex()
    {
        StringView input = new("hello");
        var second = input.Advance();
        var third = second.Advance();
        var fourth = third.Advance();
        var fifth = fourth.Advance();
        Assert.Equal('o', fifth.Peek());
    }
    
    
    [Fact]
    public void PeekCharAtNonExistingIndex()
    {
        StringView input = new("hello");
        input.Advance();
        input.Advance();
        input.Advance();
        input.Advance();
        input.Advance();
        Assert.Equal('h', input.Peek());
    }

  
}