using Xunit; 

namespace Json.Facts;

public class AnyFacts
{
    [Fact]
    public void InputIsNull()
    {
        var e = new Any("eE");

        Assert.False(e.Match(null).Success());
        Assert.Null(e.Match(null).RemainingText());
    }

    [Fact]
    public void InputIsEmpty()
    {
        var e = new Any("eE");

        Assert.False(e.Match("").Success());
        Assert.Equal("", e.Match("").RemainingText());
    }

    [Fact]
    public void ValidInputMatchesPattern()
    {
        var e = new Any("eE");

        Assert.True(e.Match("ea").Success());
        Assert.Equal("a", e.Match("ea").RemainingText());

        Assert.True(e.Match("Ea").Success());
        Assert.Equal("a", e.Match("Ea").RemainingText());
    }

    [Fact]
    public void ValidInputDoesNotMatchPattern()
    {
        var e = new Any("eE");

        Assert.False(e.Match("a").Success());
        Assert.Equal("a", e.Match("a").RemainingText());
    }

    [Fact]
    public void SignedIntegerMatchesSignsPattern()
    {
        var sign = new Any("-+");

        Assert.True(sign.Match("+3").Success());
        Assert.Equal("3", sign.Match("+3").RemainingText());

        Assert.True(sign.Match("-2").Success());
        Assert.Equal("2", sign.Match("-2").RemainingText());
    }
    
    [Fact]
    public void UnSignedIntegerDoesNotMatchSignsPattern()
    {
        var sign = new Any("-+");

        Assert.False(sign.Match("2").Success());
        Assert.Equal("2", sign.Match("2").RemainingText());
    }

    [Fact]
    public void NullInputDoesNotMatchSignPattern()
    {
        var sign = new Any("-+");
        
        Assert.False(sign.Match(null).Success());
        Assert.Null(sign.Match(null).RemainingText());
    }

    [Fact]
    public void EmptyInputDoesNotMatchSignPattern()
    {
        var sign = new Any("-+");
        
        Assert.False(sign.Match("").Success());
        Assert.Equal("", sign.Match("").RemainingText());
    }
}