using Xunit; 

namespace Json.Facts;

public class AnyFacts
{
    [Fact]
    public void InputIsNull()
    {
        Any e = new("eE");

        StringView input = new(null);
        Assert.False(e.Match(input).Success());
        Assert.Equal('\0', e.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void InputIsEmpty()
    {
        Any e = new("eE");

        StringView input = new("");
        Assert.False(e.Match(input).Success());
        Assert.Equal('\0', e.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void ValidInputMatchesPattern()
    {
        Any e = new("eE");

        StringView input = new("ea");
        Assert.True(e.Match(input).Success());
        Assert.Equal('a', e.Match(input).RemainingText().Peek());

        StringView input1 = new("Ea");
        Assert.True(e.Match(input1).Success());
        Assert.Equal('a', e.Match(input1).RemainingText().Peek());
    }

    [Fact]
    public void ValidInputDoesNotMatchPattern()
    {
        Any e = new("eE");
        StringView input = new("a");
        Assert.False(e.Match(input).Success());
        Assert.Equal('a', e.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void SignedIntegerMatchesSignsPattern()
    {
        Any sign = new("-+");

        StringView input = new("+3");
        Assert.True(sign.Match(input).Success());
        Assert.Equal('3', sign.Match(input).RemainingText().Peek());

        StringView input1 = new("-2");
        Assert.True(sign.Match(input1).Success());
        Assert.Equal('2', sign.Match(input1).RemainingText().Peek());
    }
    
    [Fact]
    public void UnSignedIntegerDoesNotMatchSignsPattern()
    {
        Any sign = new("-+");

        StringView input = new("2");
        Assert.False(sign.Match(input).Success());
        Assert.Equal('2', sign.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void NullInputDoesNotMatchSignPattern()
    {
        Any sign = new("-+");

        StringView input = new(null);
        Assert.False(sign.Match(input).Success());
        Assert.Equal('\0',sign.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void EmptyInputDoesNotMatchSignPattern()
    {
        Any sign = new("-+");
        StringView input = new("");
        Assert.False(sign.Match(input).Success());
        Assert.Equal('\0', sign.Match(input).RemainingText().Peek());
    }
}