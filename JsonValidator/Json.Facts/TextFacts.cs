#pragma warning disable CA1724

using Xunit;

namespace Json.Facts;

public class TextFacts
{
    [Fact]
    public void StringIsEqualToPattern()
    {
        Text falseString = new("false");

        StringView input = new("false");
        Assert.True(falseString.Match(input).Success());
        Assert.Equal('\0', falseString.Match(input).RemainingText().Peek());
        
        Text trueString = new("true");
        StringView input1 = new("true");

        Assert.True(trueString.Match(input1).Success());
        Assert.Equal('\0', trueString.Match(input1).RemainingText().Peek());
    }

    [Fact]
    public void StringContainsPattern()
    {
        Text falseString = new("false");
        StringView input = new("falseX");

        Assert.True(falseString.Match(input).Success());
        Assert.Equal('X', falseString.Match(input).RemainingText().Peek());
        
        Text trueString = new("true");
        StringView input1 = new("trueX");
        Assert.True(trueString.Match(input1).Success());
        Assert.Equal('X', trueString.Match(input1).RemainingText().Peek());
    }

    [Fact]
    public void StringDoesNotContainPattern()
    {
        Text falseString = new("false");
        StringView input = new("true");
        
        Assert.False(falseString.Match(input).Success());
        Assert.Equal('t', falseString.Match(input).RemainingText().Peek());
        
        Text trueString = new("true");
        StringView input2 = new("false");
        Assert.False(trueString.Match(input2).Success());
        Assert.Equal('f', trueString.Match(input2).RemainingText().Peek());
    }

    [Fact]
    public void EmptyStringDoesNotMatchNonemptyPattern()
    {
        Text trueString = new("true");
        StringView input = new("");
        
        var match = trueString.Match(input);
        Assert.False(match.Success());
    }
    

    [Fact]
    public void NullDoesNotMatchNonemptyPattern()
    {
        Text trueString = new("true");
        StringView input = new(null);
        var match = trueString.Match(input);
        Assert.False(match.Success());
    }

    [Fact]
    public void EmptyStringMatchesPattern()
    {
        Text falseString = new("false");
        StringView input = new("");
        var match = falseString.Match(input);
        Assert.False(match.Success());
        Assert.Equal('\0', match.RemainingText().Peek());
    }

    [Fact]
    public void NullDoesNotMatchPattern()
    {
        Text falseString = new("false");
        StringView input = new(null);
        Assert.False(falseString.Match(input).Success());
        Assert.Equal('\0', falseString.Match(input).RemainingText().Peek());
    }
    
    [Fact]
    public void StringMatchesEmptyPattern()
    { 
        Text empty = new("");
        StringView input = new("true");
        Assert.True(empty.Match(input).Success());
        Assert.Equal('t', empty.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void EmptyStringMatchesEmptyPattern()
    {
        Text empty = new("");
        StringView input = new("");
        Assert.True(empty.Match(input).Success());
        Assert.Equal('\0', empty.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void NullDoesNotMatchesEmptyPattern()
    {
        var empty = new Text("");
        StringView input = new(null);
        Assert.False(empty.Match(null).Success());
        Assert.Equal('\0', empty.Match(input).RemainingText().Peek());
    }
}