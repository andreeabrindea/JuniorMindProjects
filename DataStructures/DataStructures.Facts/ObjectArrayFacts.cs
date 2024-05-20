using Xunit;

namespace DataStructures.Facts;

public class ObjectArrayFacts {
    
    [Fact]
    public void AddIntStringObjectElements()
    {
        ObjectArray objectArray = new();
        
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
        ObjectArray objectArray = new();
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
        ObjectArray objectArray = new();
        
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

    [Fact]
    public void InsertNewElement()
    {
        ObjectArray objectArray = new();
        objectArray.Add(1);
        objectArray.Add("1");

        IntArray intArray = new();
        intArray.Add(1);
        intArray.Add(2);
        intArray.Add(3);
        
        objectArray.Add(intArray);
        objectArray.Add(3.14);
        
        objectArray.Insert(2, "element");
        
        Assert.Equal("element", objectArray[2]);
        Assert.Equal(2, objectArray.IndexOf("element"));
        Assert.Equal(5, objectArray.Count);
    }

    [Fact]
    public void GetIndexOfNullElementInArray()
    {
        ObjectArray objectArray = new();
        objectArray.Add(null);
        Assert.Equal(0, objectArray.IndexOf(null));
    }

    [Fact]
    public void GetElements()
    {
        var objectArray = new ObjectArray { 1, "ello", 2 };
        var elements = objectArray.GetElements();
        object[] expectedValues = new object[objectArray.Count];
        int count = 0;
        foreach (var element in elements)
        {
            expectedValues[count++] = element;
        }
        Assert.Equal(1, expectedValues[0]);
        Assert.Equal("ello", expectedValues[1]);
        Assert.Equal(2, expectedValues[2]);
        Assert.Equal(3, expectedValues.Length);
    }

    [Fact]
    public void GetElementsWhenThereIsNoElement()
    {
        var objectArray = new ObjectArray { };
        var elements = objectArray.GetElements();
        object[] expectedValues = new object[objectArray.Count];
        int count = 0;
        foreach (var element in elements)
        {
            expectedValues[count++] = element;
        }
        Assert.Empty(expectedValues);
    }
}