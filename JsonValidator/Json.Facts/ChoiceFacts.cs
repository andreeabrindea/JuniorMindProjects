using Xunit;

namespace Json.Facts;

public class ChoiceFacts
{
    [Fact]
    public void DoesNotMatchNull()
    {
        var digit = new Choice(
            new Character('0'),
            new Range('1', '9')
        );
        
        Assert.False(digit.Match(null).Success());
        Assert.Null(digit.Match(null).RemainingText());
    }
    
    [Fact]
    public void DoesNotMatchEmptyString()
    {
        var digit = new Choice(
            new Character('0'),
            new Range('1', '9')
        );
        
        Assert.False(digit.Match("").Success());
        Assert.Equal("", digit.Match("").RemainingText());
    }
    
    [Fact]
    public void IsIntegerInRange()
    {
        var digit = new Choice(
            new Character('0'),
            new Range('1', '9')
        );
        
        Assert.True(digit.Match("27").Success());
        Assert.Equal("7", digit.Match("27").RemainingText());
    }
    
    [Fact]
    public void IsIntegerNotInRange()
    {
        var digit = new Choice(
            new Character('0'),
            new Range('5', '9')
        );
        
        Assert.False(digit.Match("12").Success());
        Assert.Equal("12", digit.Match("12").RemainingText());
    }
    
    [Fact]
    public void IsStringAndHasPattern()
    {
        var pattern = new Choice(
            new Character('a'),
            new Range('5', '9')
        );
        
        Assert.True(pattern.Match("abc").Success());
        Assert.Equal("bc", pattern.Match("abc").RemainingText());
    }
    
    [Fact]
    public void IsStringAndDoesNotHavePattern()
    {
        var pattern = new Choice(
            new Character('a'),
            new Range('5', '9')
        );
        
        Assert.False(pattern.Match("bcd").Success());
        Assert.Equal("bcd", pattern.Match("bcd").RemainingText());
    }

    [Fact]
    public void HexMatchesDigitsInRange()
    {
        var digit = new Choice(
            new Character('a'),
            new Range('0', '9')
        );

        var hex = new Choice(
            digit, 
            new Choice(
                new Range('a', 'f'),
                new Range('A', 'F')
            )
        );

        Assert.True(hex.Match("012").Success());
        Assert.Equal("12", hex.Match("012").RemainingText());
    }

    [Fact]
    public void HexMatchesLowercaseLettersInRange()
    {
        var digit = new Choice(
            new Character('a'),
            new Range('5', '9')
        );

        var hex = new Choice(
            digit, 
            new Choice(
                new Range('a', 'f'),
                new Range('A', 'F')
            )
        );

        Assert.True(hex.Match("a9").Success());
        Assert.Equal("9", hex.Match("a9").RemainingText());
        
        Assert.True(hex.Match("f8").Success());
        Assert.Equal("8", hex.Match("f8").RemainingText());
    }

    [Fact]
    public void HexMatchesUppercaseLettersInRange()
    {
        var digit = new Choice(
            new Character('a'),
            new Range('5', '9')
        );

        var hex = new Choice(
            digit, 
            new Choice(
                new Range('a', 'f'),
                new Range('A', 'F')
            )
        );

        Assert.True(hex.Match("A9").Success());
        Assert.Equal("9", hex.Match("A9").RemainingText());
        
        Assert.True(hex.Match("F8").Success());
        Assert.Equal("8", hex.Match("F8").RemainingText());
    }

    [Fact]
    public void HexDoesNotMatchOutOfRangeLetters()
    {
        var digit = new Choice(
            new Character('a'),
            new Range('5', '9')
        );

        var hex = new Choice(
            digit, 
            new Choice(
                new Range('a', 'f'),
                new Range('A', 'F')
            )
        );

        Assert.False(hex.Match("g8").Success());
        Assert.Equal("g8", hex.Match("g8").RemainingText());
        
        Assert.False(hex.Match("G8").Success());
        Assert.Equal("G8", hex.Match("G8").RemainingText());
    }

    [Fact]
    public void HexDoesNotMatchEmptyString()
    {
        var digit = new Choice(
            new Character('a'),
            new Range('5', '9')
        );

        var hex = new Choice(
            digit, 
            new Choice(
                new Range('a', 'f'),
                new Range('A', 'F')
            )
        );

        Assert.False(hex.Match("").Success());
        Assert.Equal("", hex.Match("").RemainingText());
    }

    [Fact]
    public void HexDoesNotMatchNull()
    {
        var digit = new Choice(
            new Character('a'),
            new Range('5', '9')
        );

        var hex = new Choice(
            digit, 
            new Choice(
                new Range('a', 'f'),
                new Range('A', 'F')
            )
        );

        Assert.False(hex.Match(null).Success());
        Assert.Null(hex.Match(null).RemainingText());
    }
}