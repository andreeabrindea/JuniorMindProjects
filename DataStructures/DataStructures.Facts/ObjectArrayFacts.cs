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
        Assert.Equal("ello", objectArray[1]);
        Assert.Equal(typeof(IntArray), objectArray[2].GetType());
        
        Assert.Equal(3, objectArray.Count);
        
    }
}