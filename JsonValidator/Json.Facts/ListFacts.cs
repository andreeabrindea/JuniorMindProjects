using Xunit;

namespace Json.Facts;

public class ListFacts
{
    [Fact]
    public void InputStringIsElementSeparatorElementSeparatorElement()
    {
        List a = new(new Range('0', '9'), new Character(','));
        StringView input = new("1,2,3");
        var match = a.Match(input);
        Assert.True(match.Success());
        Assert.True(match.RemainingText().IsEmpty());
    }
    
    [Fact]
    public void InputStringIsElementSeparatorElementSeparatorElementSeparator()
    {
        List a = new(new Range('0', '9'), new Character(','));
        StringView input = new("1,2,3,");
        var match = a.Match(input);
        Assert.True(match.Success());
        Assert.Equal(6, match.Position().StartIndex());
    }

    [Fact]
    public void InputStringHasJustOneElementInRange()
    {
        List a = new(new Range('0', '9'), new Character(','));
        StringView input = new("123a");
        var match = a.Match(input);
        Assert.True(match.Success());
        Assert.Equal('2', match.RemainingText().Peek());
    }

    [Fact]
    public void InputStringHasNoElementsInRange()
    {
        List a = new(new Range('0', '9'), new Character(','));
        StringView input = new("abc");
        Assert.True(a.Match(input).Success());
        Assert.Equal('a', a.Match(input).RemainingText().Peek());
    }

    [Fact]
    
    public void InputStringIsEmpty()
    {
        List a = new(new Range('0', '9'), new Character(','));
        StringView input = new("");
        Assert.True(a.Match(input).Success());
        Assert.True(a.Match(input).RemainingText().IsEmpty());
    }

    [Fact]
    public void InputStringIsNull()
    {
        List a = new(new Range('0', '9'), new Character(','));
        StringView input = new(null);
        var match = a.Match(input);
        Assert.True(match.Success());
        Assert.True(match.RemainingText().IsEmpty());
    }
    
    [Fact]
    public void InputStringHasDigitsWithWhitespaceAndMultipleSeparators()
    {
        var digits = new OneOrMore(new Range('0', '9'));
        var whitespace = new Many(new Any(" \r\n\t"));
        var separator = new Sequence(whitespace, new Character(';'), whitespace);
        var list = new List(digits, separator);

        StringView input = new("1; 22  ;\n 333 \t; 22");
        var match = list.Match(input);
        Assert.True(match.Success());
        Assert.True(match.RemainingText().IsEmpty());

        StringView secondInput = new("1 \n;");
        var secondMatch = list.Match(secondInput);
        Assert.True(secondMatch.Success());
    }
}