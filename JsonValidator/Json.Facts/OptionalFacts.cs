using Xunit;

namespace Json.Facts;

public class OptionalFacts
{
    [Fact]
    public void InputStringCanBeNull()
    {
        Optional a = new(new Character('a'));
        StringView input = new(null);
        Assert.True(a.Match(input).Success());
        Assert.Equal('\0', a.Match(input).RemainingText().Peek());
    }
    
    [Fact]
    public void InputStringCanBeEmpty()
    {
        Optional a = new(new Character('a'));
        StringView input = new("");
        Assert.True(a.Match(input).Success());
        Assert.Equal('\0', a.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void InputStringContainsPattern()
    {
        Optional a = new(new Character('a'));
        StringView input = new("abc");
        Assert.True(a.Match(input).Success());
        Assert.Equal('b', a.Match(input).RemainingText().Peek());

        StringView input1 = new("aabc");
        var match = a.Match(input1);
        Assert.True(match.Success());
        Assert.Equal('a', match.RemainingText().Peek());
        
        Optional sign = new(new Character('-'));
        StringView input2 = new("-123");
        Assert.True(sign.Match(input2).Success());
        Assert.Equal('1', sign.Match(input2).RemainingText().Peek());
    }

    [Fact]
    public void InputStringDoesNotContainPattern()
    {
        var a = new Optional(new Character('a'));

        StringView input = new("bc");
        Assert.True(a.Match(input).Success());
        Assert.Equal('b', a.Match(input).RemainingText().Peek());
        
        var sign = new Optional(new Character('-'));
        StringView input1 = new("123");
        Assert.True(sign.Match(input1).Success());
        Assert.Equal('1', sign.Match(input1).RemainingText().Peek());
    }
}
