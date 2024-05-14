using Xunit;

namespace DataStructures.Facts;

public class ObjectArrayFacts {
    
    [Fact]
    public void AddIntStringObjectElements()
    {
        ObjectArray<object> objectArray = new();
        
        objectArray.Add(2);
        objectArray.Add("ello");
        
        IntArray arrayOfInteger = new();
        arrayOfInteger.Add(0);
        objectArray.Add(arrayOfInteger);
        
        Assert.Equal(2, objectArray[0]);
        Assert.True(objectArray.Contains(2));
        
        Assert.False(objectArray.Contains(3));
        
        Assert.Equal("ello", objectArray[1]);
        Assert.True(objectArray.Contains("ello"));
        
        Assert.False(objectArray.Contains("hello"));
        
        Assert.Equal(typeof(IntArray), objectArray[2].GetType());
        Assert.True(objectArray.Contains(arrayOfInteger));
        
        Assert.Equal(3, objectArray.Count);
    }


    [Fact]
    public void RemoveStringElement()
    {
        ObjectArray<object> objectArray = new();
        objectArray.Add("First element");
        objectArray.Add(1);
        objectArray.Add(2);
        objectArray.Add(3);
        
        objectArray.Remove("First element");
        Assert.False(objectArray.Contains("First element"));
        Assert.Equal(1, objectArray[0]);
        Assert.Equal(3, objectArray.Count);
    }

    [Fact]
    public void RemoveObjectElement()
    {
        ObjectArray<object> objectArray = new();
        
        IntArray intArray = new();
        intArray.Add(10);
        intArray.Add(30);
        intArray.Add(20);

        SortedIntArray sortedIntArray = new();
        sortedIntArray.Add(60);
        sortedIntArray.Add(40);
        sortedIntArray.Add(50);
        
        objectArray.Add(intArray);
        objectArray.Add(sortedIntArray);
        
        Assert.Equal(2, objectArray.Count);
        
        objectArray.Remove(intArray);
        Assert.Equal(1, objectArray.Count);
        Assert.Equal(typeof(SortedIntArray), objectArray[0].GetType());
    }
}