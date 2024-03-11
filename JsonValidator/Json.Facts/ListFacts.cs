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
        Assert.Equal('\0', match.RemainingText().Peek());
    }
    
    [Fact]
    public void InputStringIsElementSeparatorElementSeparatorElementSeparator()
    {
        List a = new(new Range('0', '9'), new Character(','));
        StringView input = new("1,2,3,");
        var match = a.Match(input);
        Assert.True(match.Success());
        Assert.Equal(',', match.RemainingText().Peek());
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
        Assert.Equal('\0', a.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void InputStringIsNull()
    {
        List a = new(new Range('0', '9'), new Character(','));
        StringView input = new(null);
        var match = a.Match(input);
        Assert.True(match.Success());
        Assert.Equal('\0', match.RemainingText().Peek());
    }
    
    [Fact]
    public void InputStringHasDigitsWithWhitespaceAndMultipleSeparators()
    {
        OneOrMore digits = new(new Range('0', '9'));
        Many whitespace = new(new Any(" \r\n\t"));
        Sequence separator = new(whitespace, new Character(';'), whitespace);
        List list = new(digits, separator);

        StringView input = new("1; 22  ;\n 333 \t; 22");
        var match = list.Match(input);
        Assert.True(match.Success());
        Assert.Equal('\0', match.RemainingText().Peek());

        StringView input1 = new("1 \n;");
        var match1 = list.Match(input1);
        Assert.True(match1.Success());
        //Assert.Equal('\n',  match1.RemainingText().Peek());
    }
}