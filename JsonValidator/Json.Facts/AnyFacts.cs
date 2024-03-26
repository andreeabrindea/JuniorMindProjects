using Xunit; 

namespace Json.Facts;

public class AnyFacts
{
    [Fact]
    public void InputIsNull()
    {
        Any e = new("eE");

        StringView input = new(null);
        var match = e.Match(input);
        Assert.False(match.Success());
        Assert.True(match.RemainingText().IsEmpty());
        Assert.Equal(0, match.Position());
    }

    [Fact]
    public void InputIsEmpty()
    {
        Any e = new("eE");

        StringView input = new("");
        
        var match = e.Match(input);
        Assert.False(match.Success());
        Assert.True(match.RemainingText().IsEmpty());
        Assert.Equal(0, match.Position());
    }

    [Fact]
    public void ValidInputMatchesPattern()
    {
        Any e = new("eE");

        StringView input = new("ea");
        
        var match = e.Match(input);
        Assert.True(match.Success());
        Assert.Equal('a', match.RemainingText().Peek());
        Assert.Equal(1, match.Position());

        StringView secondInput = new("Ea");

        var secondMatch = e.Match(secondInput);
        Assert.True(secondMatch.Success());
        Assert.Equal('a', secondMatch.RemainingText().Peek());
        Assert.Equal(1, secondMatch.Position());
    }

    [Fact]
    public void ValidInputDoesNotMatchPattern()
    {
        Any e = new("eE");
        StringView input = new("a");

        var match = e.Match(input);
        Assert.False(match.Success());
        Assert.Equal('a', match.RemainingText().Peek());
        Assert.Equal(0, match.Position());
    }

    [Fact]
    public void SignedIntegerMatchesSignsPattern()
    {
        Any sign = new("-+");

        StringView input = new("+3");

        var match = sign.Match(input);
        Assert.True(match.Success());
        Assert.Equal('3', match.RemainingText().Peek());
        Assert.Equal(1, match.Position());

        StringView secondInput = new("-2");

        var secondMatch = sign.Match(secondInput);
        Assert.True(secondMatch.Success());
        Assert.Equal('2', secondMatch.RemainingText().Peek());
        Assert.Equal(1, secondMatch.Position());
    }
    
    [Fact]
    public void UnSignedIntegerDoesNotMatchSignsPattern()
    {
        Any sign = new("-+");

        StringView input = new("2");
        
        var match = sign.Match(input);
        Assert.False(match.Success());
        Assert.Equal('2', match.RemainingText().Peek());
        Assert.Equal(0, match.Position());
    }

    [Fact]
    public void NullInputDoesNotMatchSignPattern()
    {
        Any sign = new("-+");

        StringView input = new(null);
        Assert.False(sign.Match(input).Success());
        Assert.True(sign.Match(input).RemainingText().IsEmpty());
    }

    [Fact]
    public void EmptyInputDoesNotMatchSignPattern()
    {
        Any sign = new("-+");
        StringView input = new("");
        var match = sign.Match(input);
        Assert.False(match.Success());
        Assert.True(match.RemainingText().IsEmpty());
    }
}