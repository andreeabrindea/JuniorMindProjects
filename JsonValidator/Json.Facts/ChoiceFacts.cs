using Xunit;

namespace Json.Facts;

public class ChoiceFacts
{
    [Fact]
    public void DoesNotMatchNull()
    {
        Choice digit = new(
            new Character('0'),
            new Range('1', '9')
        );
        
        StringView input = new(null);
        Assert.False(digit.Match(input).Success());
    }
    
    [Fact]
    public void DoesNotMatchEmptyString()
    {
        Choice digit = new(
            new Character('0'),
            new Range('1', '9')
        );
        
        StringView input = new("");
        Assert.False(digit.Match(input).Success());
    }
    
    [Fact]
    public void IsIntegerInRange()
    {
        Choice digit = new(
            new Character('0'),
            new Range('1', '9')
        );
        
        StringView input = new("27");
        var match = digit.Match(input);
        Assert.True(match.Success());
        Assert.Equal('7', match.RemainingText().Peek());
    }
    
    [Fact]
    public void IsIntegerNotInRange()
    {
        Choice digit = new(
            new Character('0'),
            new Range('5', '9')
        );

        StringView input = new("12");
        Assert.False(digit.Match(input).Success());
        Assert.Equal('1', digit.Match(input).RemainingText().Peek());
    }
    
    [Fact]
    public void IsStringAndHasPattern()
    {
        Choice pattern = new(
            new Character('a'),
            new Range('5', '9')
        );

        StringView input = new("abc");
        Assert.True(pattern.Match(input).Success());
        Assert.Equal('b', pattern.Match(input).RemainingText().Peek());
    }
    
    [Fact]
    public void IsStringAndDoesNotHavePattern()
    {
        Choice pattern = new(
            new Character('a'),
            new Range('5', '9')
        );

        StringView input = new("bcd");
        Assert.False(pattern.Match(input).Success());
        Assert.Equal('b', pattern.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void HexMatchesDigitsInRange()
    {
        Choice digit = new(
            new Character('a'),
            new Range('0', '9')
        );

        Choice hex = new(
            digit, 
            new Choice(
                new Range('a', 'f'),
                new Range('A', 'F')
            )
        );

        StringView input = new("012");
        var match = hex.Match(input);
        Assert.True(match.Success());
        Assert.Equal('1', match.RemainingText().Peek());
    }

    [Fact]
    public void HexMatchesLowercaseLettersInRange()
    {
        Choice digit = new(
            new Character('a'),
            new Range('5', '9')
        );

        Choice hex = new(
            digit, 
            new Choice(
                new Range('a', 'f'),
                new Range('A', 'F')
            )
        );

        StringView input = new("a9");
        var match = hex.Match(input);
        Assert.True(match.Success());
        Assert.Equal('9', match.RemainingText().Peek());

        StringView secondInput = new("f8");
        var secondMatch = hex.Match(secondInput);
        Assert.True(secondMatch.Success());
        Assert.Equal('8', secondMatch.RemainingText().Peek());
    }

    [Fact]
    public void HexMatchesUppercaseLettersInRange()
    {
        Choice digit = new(
            new Character('a'),
            new Range('5', '9')
        );

        Choice hex = new(
            digit, 
            new Choice(
                new Range('a', 'f'),
                new Range('A', 'F')
            )
        );

        StringView input = new("A9");
        var match = hex.Match(input);
        Assert.True(match.Success());
        Assert.Equal('9', match.RemainingText().Peek());

        StringView secondInput = new("F8");
        var secondMatch = hex.Match(secondInput);
        Assert.True(secondMatch.Success());
        Assert.Equal('8', secondMatch.RemainingText().Peek());
    }

    [Fact]
    public void HexDoesNotMatchOutOfRangeLetters()
    {
        Choice digit = new(
            new Character('a'),
            new Range('5', '9')
        );

        Choice hex = new(
            digit, 
            new Choice(
                new Range('a', 'f'),
                new Range('A', 'F')
            )
        );

        StringView input = new("g8");
        Assert.False(hex.Match(input).Success());
        Assert.Equal('g', hex.Match(input).RemainingText().Peek());

        StringView secondInput = new("G8");
        Assert.False(hex.Match(secondInput).Success());
        Assert.Equal('G', hex.Match(secondInput).RemainingText().Peek());
    }

    [Fact]
    public void HexDoesNotMatchEmptyString()
    {
        Choice digit = new(
            new Character('a'),
            new Range('5', '9')
        );

        Choice hex = new(
            digit, 
            new Choice(
                new Range('a', 'f'),
                new Range('A', 'F')
            )
        );

        StringView input = new("");
        Assert.False(hex.Match(input).Success());
    }

    [Fact]
    public void HexDoesNotMatchNull()
    {
        Choice digit = new(
            new Character('a'),
            new Range('5', '9')
        );

        Choice hex = new(
            digit, 
            new Choice(
                new Range('a', 'f'),
                new Range('A', 'F')
            )
        );
        StringView input = new(null);
        Assert.False(hex.Match(input).Success());
    }

    [Fact]
    public void InputMatchesAfterAddingNewPattern()
    {
        Choice pattern = new(new Range('0', '2'), new Character('a'));

        StringView input = new("5");
        Assert.False(pattern.Match(input).Success());
        Assert.Equal('5', pattern.Match(input).RemainingText().Peek());
        
        pattern.Add(new Range('3', '9'));
        Assert.True(pattern.Match(input).Success());
    }
    
    [Fact]
    public void InputDoesNotMatchAfterAddingNewPattern()
    {
        Choice pattern = new(new Range('0', '2'), new Character('a'));
        StringView input = new("z");
        Assert.False(pattern.Match(input).Success());
        Assert.Equal('z', pattern.Match(input).RemainingText().Peek());
        
        pattern.Add(new Range('3', '9'));
        Assert.False(pattern.Match(input).Success());
        Assert.Equal('z', pattern.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void PatternDoesNotMatchNullAfterAddingPattern()
    {
        Choice pattern = new(new Character('a'), new Character('b'));
        pattern.Add(new Character('c'));

        StringView input = new(null);
        Assert.False(pattern.Match(input).Success());
    }
    
    [Fact]
    public void PatternDoesNotMatchEmptyStringAfterAddingPattern()
    {
        Choice pattern = new(new Character('a'), new Character('b'));
        pattern.Add(new Character('c'));
        
        StringView input = new(string.Empty);
        Assert.False(pattern.Match(input).Success());
    }

    [Fact]
    public void AddingNullPattern()
    {
        Choice pattern = new (new Character('a'));
        pattern.Add(null);
        StringView input = new("abc");
        Assert.True(pattern.Match(input).Success());
        Assert.Equal('b', pattern.Match(input).RemainingText().Peek());
    }
}