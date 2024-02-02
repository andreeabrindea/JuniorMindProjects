using Xunit;

namespace Json.Facts;

public class TextPatternFacts
{
    [Fact]
    public void StringIsEqualToPattern()
    {
        var falseString = new TextPattern("false");

        Assert.True(falseString.Match("false").Success());
        Assert.Equal("", falseString.Match("false").RemainingText());
        
        var trueString = new TextPattern("true");

        Assert.True(trueString.Match("true").Success());
        Assert.Equal("", trueString.Match("true").RemainingText());
    }

    [Fact]
    public void StringContainsPattern()
    {
        var falseString = new TextPattern("false");

        Assert.True(falseString.Match("falseX").Success());
        Assert.Equal("X", falseString.Match("falseX").RemainingText());
        
        var trueString = new TextPattern("true");

        Assert.True(trueString.Match("trueX").Success());
        Assert.Equal("X", trueString.Match("trueX").RemainingText());
    }

    [Fact]
    public void StringDoesNotContainPattern()
    {
        var falseString = new TextPattern("false");

        Assert.False(falseString.Match("true").Success());
        Assert.Equal("true", falseString.Match("true").RemainingText());
        
        var trueString = new TextPattern("true");
        
        Assert.False(trueString.Match("false").Success());
        Assert.Equal("false", trueString.Match("false").RemainingText());
    }

    [Fact]
    public void EmptyStringDoesNotMatchNonemptyPattern()
    {
        var trueString = new TextPattern("true");

        Assert.False(trueString.Match("").Success());
        Assert.Equal("false", trueString.Match("false").RemainingText());
    }
    

    [Fact]
    public void NullDoesNotMatchNonemptyPattern()
    {
        var trueString = new TextPattern("true");

        Assert.False(trueString.Match(null).Success());
        Assert.Null(trueString.Match(null).RemainingText());
    }

    [Fact]
    public void EmptyStringMatchesPattern()
    {
        var falseString = new TextPattern("false");

        Assert.False(falseString.Match("").Success());
        Assert.Equal("", falseString.Match("").RemainingText());
    }

    [Fact]
    public void NullDoesNotMatchPattern()
    {
        var falseString = new TextPattern("false");

        Assert.False(falseString.Match(null).Success());
        Assert.Null(falseString.Match(null).RemainingText());
    }
    
    [Fact]
    public void StringMatchesEmptyPattern()
    { 
        var empty = new TextPattern("");
        
        Assert.True(empty.Match("true").Success());
        Assert.Equal("true", empty.Match("true").RemainingText());
    }

    [Fact]
    public void EmptyStringMatchesEmptyPattern()
    {
        var empty = new TextPattern("");

        Assert.True(empty.Match("").Success());
        Assert.Equal("", empty.Match("").RemainingText());
    }

    [Fact]
    public void NullDoesNotMatchesEmptyPattern()
    {
        var empty = new TextPattern("");
        
        Assert.False(empty.Match(null).Success());
        Assert.Null(empty.Match(null).RemainingText());
    }
}