using Xunit;
namespace Json.Facts;

public class ChoiceFacts
{
    [Fact]
    public void IsNull()
    {
        var digit = new Choice(
            new Character('0'),
            new Range('1', '9')
        );
        
        Assert.False(digit.Match(null));
    }
    
    [Fact]
    public void IsEmpty()
    {
        var digit = new Choice(
            new Character('0'),
            new Range('1', '9')
        );
        
        Assert.False(digit.Match(""));
    }
    
    [Fact]
    public void IsIntegerInRange()
    {
        var digit = new Choice(
            new Character('0'),
            new Range('1', '9')
        );
        
        Assert.True(digit.Match("27"));
    }
    
    [Fact]
    public void IsIntegerNotInRange()
    {
        var digit = new Choice(
            new Character('0'),
            new Range('5', '9')
        );
        
        Assert.False(digit.Match("12"));
    }
    
    [Fact]
    public void IsStringAndHasPattern()
    {
        var digit = new Choice(
            new Character('a'),
            new Range('5', '9')
        );
        
        Assert.True(digit.Match("abc"));
    }
    
    [Fact]
    public void IsStringAndDoesNotHavePattern()
    {
        var digit = new Choice(
            new Character('a'),
            new Range('5', '9')
        );
        
        Assert.False(digit.Match("bcd"));
    }
}