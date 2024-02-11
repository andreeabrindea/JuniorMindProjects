using Xunit;

namespace Json.Facts;

public class ListFacts
{
    [Fact]
    public void InputStringIsElementSeparatorElementSeparatorElement()
    {
        var a = new List(new Range('0', '9'), new Character(','));
        Assert.True(a.Match("1,2,3").Success());
        Assert.Equal("", a.Match("1,2,3").RemainingText());
    }
    
    [Fact]
    public void InputStringIsElementSeparatorElementSeparatorElementSeparator()
    {
        var a = new List(new Range('0', '9'), new Character(','));

        Assert.True(a.Match("1,2,3,").Success());
        Assert.Equal(",", a.Match("1,2,3,").RemainingText());
    }

    [Fact]
    public void InputStringHasJustOneElementInRange()
    {
        var a = new List(new Range('0', '9'), new Character(','));

        Assert.True(a.Match("123a").Success());
        Assert.Equal("a", a.Match("123a").RemainingText());
    }

    [Fact]
    public void InputStringHasNoElementsInRange()
    {
        var a = new List(new Range('0', '9'), new Character(','));

        Assert.True(a.Match("abc").Success());
        Assert.Equal("abc", a.Match("abc").RemainingText());
    }

    [Fact]
    public void InputStringIsEmpty()
    {
        var a = new List(new Range('0', '9'), new Character(','));

        Assert.True(a.Match("").Success());
        Assert.Equal("", a.Match("").RemainingText());
    }

    [Fact]
    public void InputStringIsNull()
    {
        var a = new List(new Range('0', '9'), new Character(','));

        Assert.True(a.Match(null).Success());
        Assert.Null(a.Match(null).RemainingText());
    }
    
    [Fact]
    public void InputStringHasDigitsWithWhitespaceAndMultipleSeparators()
    {
    var digits = new OneOrMore(new Range('0', '9'));
        var whitespace = new Many(new Any(" \r\n\t"));
        var separator = new Sequence(whitespace, new Character(';'), whitespace);
        var list = new List(digits, separator);

        Assert.True(list.Match("1; 22  ;\n 333 \t; 22").Success());
        Assert.Equal("", list.Match("1; 22  ;\n 333 \t; 22").RemainingText());
        
        Assert.True(list.Match("1 \n;").Success());
        Assert.Equal(" \n;",  list.Match("1 \n;").RemainingText());
    }
}