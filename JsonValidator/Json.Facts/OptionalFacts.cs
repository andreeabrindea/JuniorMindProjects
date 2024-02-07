using Xunit;

namespace Json.Facts;

public class OptionalFacts
{
    [Fact]
    public void InputStringCanBeNull()
    {
        var a = new Optional(new Character('a'));

        Assert.True(a.Match(null).Success());
        Assert.Null(a.Match(null).RemainingText());
    }
    
    [Fact]
    public void InputStringCanBeEmpty()
    {
        var a = new Optional(new Character('a'));
        
        Assert.True(a.Match("").Success());
        Assert.Equal("", a.Match("").RemainingText());
    }

    [Fact]
    public void InputStringContainsPattern()
    {
        var a = new Optional(new Character('a'));

        Assert.True(a.Match("abc").Success());
        Assert.Equal("bc", a.Match("abc").RemainingText());

        Assert.True(a.Match("aabc").Success());
        Assert.Equal("abc", a.Match("aabc").RemainingText());
        
        var sign = new Optional(new Character('-'));

        Assert.True(sign.Match("-123").Success());
        Assert.Equal("123", sign.Match("-123").RemainingText());
    }

    [Fact]
    public void InputStringDoesNotContainPattern()
    {
        var a = new Optional(new Character('a'));

        Assert.True(a.Match("bc").Success());
        Assert.Equal("bc", a.Match("bc").RemainingText());
        
        var sign = new Optional(new Character('-'));

        Assert.True(sign.Match("123").Success());
        Assert.Equal("123", sign.Match("123").RemainingText());
        
    }
}
