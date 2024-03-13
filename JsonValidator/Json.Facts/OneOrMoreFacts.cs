using Xunit;
namespace Json.Facts;

public class OneOrMoreFacts
{
    [Fact]
    public void InputStringIsNull()
    {
        OneOrMore a = new(new Range('0', '9'));
        StringView input = new(null);
        Assert.False(a.Match(input).Success());
    }

    [Fact]
    public void InputStringIsEmpty()
    {
        OneOrMore a = new(new Range('0', '9'));
        StringView input = new("");
        Assert.False(a.Match(input).Success());
    }

    [Fact]
    public void InputStringDoesNotMatchPatternOnce()
    {
        OneOrMore a = new(new Range('0', '9'));
        StringView input = new("bc");
        Assert.False(a.Match(input).Success());
        Assert.Equal('b', a.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void InputStringMatchesOnce()
    {
        OneOrMore a = new(new Range('0', '9'));
        StringView input = new("1a");
        var match = a.Match(input);
        Assert.True(match.Success());
        Assert.Equal('a', match.RemainingText().Peek());
    }
    
    [Fact]
    public void InputStringMatchesForAllCharacters()
    {
        OneOrMore a = new(new Range('0', '9'));
        StringView input = new("123");
        Assert.True(a.Match(input).Success());
    }

}