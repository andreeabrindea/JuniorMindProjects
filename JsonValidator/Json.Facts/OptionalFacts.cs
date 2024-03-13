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
        Assert.True(a.Match(input).RemainingText().IsEmpty());
    }
    
    [Fact]
    public void InputStringCanBeEmpty()
    {
        Optional a = new(new Character('a'));
        StringView input = new("");
        Assert.True(a.Match(input).Success());
        Assert.True(a.Match(input).RemainingText().IsEmpty());
    }

    [Fact]
    public void InputStringContainsPattern()
    {
        Optional a = new(new Character('a'));
        StringView input = new("abc");
        Assert.True(a.Match(input).Success());
        Assert.Equal('b', a.Match(input).RemainingText().Peek());

        StringView secondInput = new("aabc");
        var match = a.Match(secondInput);
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
        StringView secondInput = new("123");
        Assert.True(sign.Match(secondInput).Success());
        Assert.Equal('1', sign.Match(secondInput).RemainingText().Peek());
    }
}
