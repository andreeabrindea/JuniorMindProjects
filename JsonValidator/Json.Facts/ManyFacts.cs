using Xunit;

namespace Json.Facts;

public class ManyFacts
{
    [Fact]
    public void StringContainsPatternOneTime()
    {
        Many a = new(new Character('a'));
        StringView input = new("abc");
        Assert.True(a.Match(input).Success());
        Assert.Equal('b', a.Match(input).RemainingText().Peek());
    }
    
    [Fact]
    public void StringContainsPatternSeveralTimes()
    {
        Many a = new(new Character('a'));
        StringView input = new("aaaabc");
        Assert.True(a.Match(input).Success());
        Assert.Equal('b', a.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void StringNotContainingPatternMatches()
    {
        Many a = new(new Character('a'));
        StringView input = new("bc");
        Assert.True(a.Match(input).Success());
        Assert.Equal('b', a.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void EmptyStringMatchesPattern()
    {
        Many a = new(new Character('a'));
        StringView input = new("");
        Assert.True(a.Match(input).Success());
    }

    [Fact]
    public void NullMatchesPattern()
    {
        Many a = new(new Character('a'));
        StringView input = new(null);
        var match = a.Match(input);
        Assert.True(match.Success());
    }

    [Fact]
    public void StringNotInRangePattern()
    {
        Many digits = new(new Range('0', '9'));
        StringView input = new("ab");
        Assert.True(digits.Match(input).Success());
        Assert.Equal('a', digits.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void StringInRangePattern()
    {
        Many digits = new(new Range('0', '9'));
        StringView input = new("12345ab123");
        Assert.True(digits.Match(input).Success());
        Assert.Equal('a', digits.Match(input).RemainingText().Peek());
    }
}