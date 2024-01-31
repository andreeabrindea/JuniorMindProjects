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
    }
    
    [Fact]
    public void DoesNotMatchEmptyString()
    {
        var digit = new Choice(
            new Character('0'),
            new Range('1', '9')
        );
        
        Assert.False(digit.Match("").Success());
    }
    
    [Fact]
    public void IsIntegerInRange()
    {
        var digit = new Choice(
            new Character('0'),
            new Range('1', '9')
        );
        
        Assert.True(digit.Match("27").Success());
    }
    
    [Fact]
    public void IsIntegerNotInRange()
    {
        var digit = new Choice(
            new Character('0'),
            new Range('5', '9')
        );
        
        Assert.False(digit.Match("12").Success());
    }
    
    [Fact]
    public void IsStringAndHasPattern()
    {
        var digit = new Choice(
            new Character('a'),
            new Range('5', '9')
        );
        
        Assert.True(digit.Match("abc").Success());
    }
    
    [Fact]
    public void IsStringAndDoesNotHavePattern()
    {
        var digit = new Choice(
            new Character('a'),
            new Range('5', '9')
        );
        
        Assert.False(digit.Match("bcd").Success());
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
        Assert.True(hex.Match("f8").Success());
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
        Assert.True(hex.Match("F8").Success());
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
        Assert.False(hex.Match("G8").Success());
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
    }
}