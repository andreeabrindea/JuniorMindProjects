using Xunit; 

namespace Json.Facts;

public class RemainingTextFacts 
{
    [Fact]
    public void Tests()
    {
        string ceva = "hello";
        var auxClass = new RemainingText(ceva);
        auxClass.IncrementIndex();
        Assert.Equal("ello", auxClass.RemoveSubstringFromIndex());
    }
}