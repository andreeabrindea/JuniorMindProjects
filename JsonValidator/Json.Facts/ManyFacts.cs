using Xunit;

namespace Json.Facts;

public class ManyFacts
{
    [Fact]
    public void StringContainsPatternOneTime()
    {
        var a = new Many(new Character('a'));
        
        Assert.True(a.Match("abc").Success());
        Assert.Equal("bc", a.Match("abc").RemainingText());
    }
    
    [Fact]
    public void StringContainsPatternSeveralTimes()
    {
        var a = new Many(new Character('a'));
     
        Assert.True(a.Match("aaaabc").Success());
        Assert.Equal("bc", a.Match("aaaabc").RemainingText());
    }

    [Fact]
    public void StringNotContainingPatternMatches()
    {
        var a = new Many(new Character('a'));

        Assert.True(a.Match("bc").Success());
        Assert.Equal("bc", a.Match("bc").RemainingText());
    }

    [Fact]
    public void EmptyStringMatchesPattern()
    {
        var a = new Many(new Character('a'));

        Assert.True(a.Match("").Success());
        Assert.Equal("", a.Match("").RemainingText());
    }

    [Fact]
    public void NullMatchesPattern()
    {
        var a = new Many(new Character('a'));

        Assert.True(a.Match(null).Success());
        Assert.Null(a.Match(null).RemainingText());
    }

    [Fact]
    public void StringNotInRangePattern()
    {
        var digits = new Many(new Range('0', '9'));

        Assert.True(digits.Match("ab").Success());
        Assert.Equal("ab", digits.Match("ab").RemainingText());
    }

    [Fact]
    public void StringInRangePattern()
    {
        var digits = new Many(new Range('0', '9'));

        Assert.True(digits.Match("12345ab123").Success());
        Assert.Equal("ab123", digits.Match("12345ab123").RemainingText());
    }
}