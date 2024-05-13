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

    [Fact]
    public void Test_AddMultipleElements()
    {
        SortedIntArray sortedIntArray = new SortedIntArray();
        sortedIntArray.Add(59);
        sortedIntArray.Add(7);
        sortedIntArray.Add(15);
        sortedIntArray.Add(43);
        sortedIntArray.Add(12);
        sortedIntArray.Add(6);
        
        Assert.Equal(6, sortedIntArray[0]);
        Assert.Equal(7, sortedIntArray[1]);
        Assert.Equal(12, sortedIntArray[2]);
        Assert.Equal(15, sortedIntArray[3]);
        Assert.Equal(43, sortedIntArray[4]);
        Assert.Equal(59, sortedIntArray[5]);
        Assert.Equal(0, sortedIntArray.IndexOf(6));
    }
    
}