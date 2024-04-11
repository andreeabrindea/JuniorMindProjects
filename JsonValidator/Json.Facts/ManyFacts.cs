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
        Assert.True(a.Match(input).RemainingText().IsEmpty());
    }

    [Fact]
    public void NullMatchesPattern()
    {
        Many a = new(new Character('a'));
        StringView input = new(null);
        var match = a.Match(input);
        Assert.True(match.Success());
        Assert.True(match.RemainingText().IsEmpty());
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

        var match = digits.Match(input);
        Assert.True(match.Success());
        Assert.Equal('a', match.RemainingText().Peek());
    }

    [Fact]
    public void testManyOnText()
    {
        Text textPattern = new("aaaaa");
        Many many = new(textPattern);
        StringView input = new("aaaab");

        var match = many.Match(input);
        Assert.True(match.Success());
        Assert.Equal(0, match.RemainingText().StartIndex());
        Assert.Equal(5, match.Position().StartIndex());
    }
}