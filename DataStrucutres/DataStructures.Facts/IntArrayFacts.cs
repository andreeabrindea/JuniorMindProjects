using Xunit;

namespace DataStructures.Facts;

public class IntArrayFacts
{
    [Fact]
    public void Test_AddingNewElementAtFirstPosition()
    {
        IntArray array = new();
        array.Add(3);
        
        Assert.True(array.Contains(3));
        Assert.Equal(0, array.IndexOf(3));
        Assert.Equal(3, array.Element(0));
    }

    [Fact]
    public void Test_InsertANewElementToPosition()
    {
        IntArray array = new();
        array.Add(1);
        array.Add(2);
        
        array.Insert(1, 7);
        
        Assert.True(array.Contains(7));
        Assert.Equal(1, array.IndexOf(7));
        Assert.Equal(7, array.Element(1));
    }

    [Fact]
    public void Test_RemoveFromExistingPosition()
    {
        IntArray array = new();
        array.Add(0);
        array.Add(1);
        array.Add(2);
        
        array.RemoveAt(1);
        
        Assert.Equal(0, array.Element(0));
        Assert.Equal(2, array.Element(1));
        Assert.Equal(-1, array.IndexOf(3));
        Assert.Equal(2, array.Count());
    }

    [Fact]
    public void TestRemoveElement()
    {
        IntArray array = new();
        array.Add(0);
        array.Add(1);
        array.Add(2);
        
        array.Remove(1);
        Assert.Equal(0, array.Element(0));
        Assert.Equal(2, array.Element(1));
        Assert.Equal(-1, array.IndexOf(3));
        Assert.Equal(2, array.Count());
    }
    
}