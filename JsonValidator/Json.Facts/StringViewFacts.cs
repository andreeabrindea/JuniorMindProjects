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
        input.Advance();
        input.Advance();
        input.Advance();
        input.Advance();
        Assert.Equal('o', input.Peek());
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
        Assert.Equal('\0', input.Peek());
    }

  
}