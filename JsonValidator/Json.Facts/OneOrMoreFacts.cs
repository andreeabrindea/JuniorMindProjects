using Xunit;
namespace Json.Facts;

public class OneOrMoreFacts
{
    [Fact]
    public void InputStringIsNull()
    {
        var a = new OneOrMore(new Range('0', '9'));

        Assert.False(a.Match(null).Success());
        Assert.Null(a.Match(null).RemainingText());
    }

    [Fact]
    public void InputStringIsEmpty()
    {
        var a = new OneOrMore(new Range('0', '9'));
        
        Assert.False(a.Match("").Success());
        Assert.Equal("", a.Match("").RemainingText());
    }

    [Fact]
    public void InputStringDoesNotMatchPatternOnce()
    {
        var a = new OneOrMore(new Range('0', '9'));

        Assert.False(a.Match("bc").Success());
        Assert.Equal("bc", a.Match("bc").RemainingText());
    }

    [Fact]
    public void InputStringMatchesOnce()
    {
        var a = new OneOrMore(new Range('0', '9'));
        
        Assert.True(a.Match("1a").Success());
        Assert.Equal("a", a.Match("1a").RemainingText());
    }
    
    [Fact]
    public void InputStringMatchesForAllCharacters()
    {
        var a = new OneOrMore(new Range('0', '9'));

        Assert.True(a.Match("123").Success());
        Assert.Equal("", a.Match("123").RemainingText());
        
    }

}