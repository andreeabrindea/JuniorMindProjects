using Xunit;

namespace DataStructures.Facts;

public class SortedIntArrayFacts
{
    [Fact]
    public void AddASingleElement()
    {
        SortedIntArray sortedIntArray = new();
        sortedIntArray.Add(5);
        
        Assert.Equal(5, sortedIntArray[0]);
        Assert.Equal(0, sortedIntArray.IndexOf(5));
    }

    [Fact]
    public void AddMultipleElements()
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
    
    [Fact]
    public void RemoveFromExistingPosition()
    {
        SortedIntArray sortedIntArray = new();
        sortedIntArray.Add(80);
        sortedIntArray.Add(10);
        sortedIntArray.Add(20);
        
        sortedIntArray.RemoveAt(1);
        
        Assert.Equal(10, sortedIntArray[0]);
        Assert.Equal(80, sortedIntArray[1]);
        Assert.Equal(-1, sortedIntArray.IndexOf(20));
        Assert.Equal(2, sortedIntArray.Count);
    }

    [Fact]
    public void InsertAtFirstPosition()
    {
        SortedIntArray sortedIntArray = new();
        sortedIntArray.Add(32);
        sortedIntArray.Add(12);
        sortedIntArray.Add(20);
        sortedIntArray.Add(18);
        
        sortedIntArray.Insert(0, 90);
        
        Assert.Equal(12, sortedIntArray[0]);
        Assert.Equal(0, sortedIntArray.IndexOf(12));
        
        Assert.Equal(18, sortedIntArray[1]);
        Assert.Equal(1, sortedIntArray.IndexOf(18));
        
        Assert.Equal(20, sortedIntArray[2]);
        Assert.Equal(2, sortedIntArray.IndexOf(20));
        
        Assert.Equal(32, sortedIntArray[3]);
        Assert.Equal(3, sortedIntArray.IndexOf(32));
        
        Assert.True(sortedIntArray.Contains(90));
        Assert.Equal(90, sortedIntArray[4]);
        Assert.Equal(4, sortedIntArray.IndexOf(90));
    }
    
}