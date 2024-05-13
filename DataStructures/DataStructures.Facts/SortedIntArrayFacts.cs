using Xunit;

namespace DataStructures.Facts;

public class SortedIntArrayFacts
{
    [Fact]
    public void Test_AddASingleElement()
    {
        SortedIntArray sortedIntArray = new();
        sortedIntArray.Add(5);
        
        Assert.Equal(5, sortedIntArray[0]);
        Assert.Equal(0, sortedIntArray.IndexOf(5));
    }
    
}