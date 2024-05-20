using Xunit;

namespace DataStructures.Facts;

public class SortedIntArrayFacts
{
    [Fact]
    public void AddASingleElement()
    {
        SortedList<int> sortedList = new();
        sortedList.Add(5);
        
        Assert.Equal(5, sortedList[0]);
        Assert.Equal(0, sortedList.IndexOf(5));
    }

    [Fact]
    public void AddMultipleElements()
    {
        SortedList<int> sortedList = new ();
        sortedList.Add(59);
        sortedList.Add(7);
        sortedList.Add(15);
        sortedList.Add(43);
        sortedList.Add(12);
        sortedList.Add(6);
        
        Assert.Equal(6, sortedList[0]);
        Assert.Equal(7, sortedList[1]);
        Assert.Equal(12, sortedList[2]);
        Assert.Equal(15, sortedList[3]);
        Assert.Equal(43, sortedList[4]);
        Assert.Equal(59, sortedList[5]);
        Assert.Equal(0, sortedList.IndexOf(6));
    }
    
    [Fact]
    public void RemoveFromExistingPosition()
    {
        SortedList<int> sortedList = new();
        sortedList.Add(80);
        sortedList.Add(10);
        sortedList.Add(20);
        
        sortedList.RemoveAt(1);
        
        Assert.Equal(10, sortedList[0]);
        Assert.Equal(80, sortedList[1]);
        Assert.Equal(-1, sortedList.IndexOf(20));
        Assert.Equal(2, sortedList.Count);
    }

    [Fact]
    public void InsertAtFirstPosition()
    {
        SortedList<int> sortedList = new();
        sortedList.Add(32);
        sortedList.Add(12);
        sortedList.Add(20);
        sortedList.Add(18);
        
        sortedList.Insert(0, 90);
        
        Assert.Equal(12, sortedList[0]);
        Assert.Equal(0, sortedList.IndexOf(12));
        
        Assert.Equal(18, sortedList[1]);
        Assert.Equal(1, sortedList.IndexOf(18));
        
        Assert.Equal(20, sortedList[2]);
        Assert.Equal(2, sortedList.IndexOf(20));
        
        Assert.Equal(32, sortedList[3]);
        Assert.Equal(3, sortedList.IndexOf(32));
        
        Assert.True(sortedList.Contains(90));
        Assert.Equal(90, sortedList[4]);
        Assert.Equal(4, sortedList.IndexOf(90));
    }
    
}