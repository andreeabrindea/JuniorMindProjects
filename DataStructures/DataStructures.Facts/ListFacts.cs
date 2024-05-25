using Xunit;

namespace DataStructures.Facts;

public class ListFacts {
    
    [Fact]
    public void AddIntStringObjectElements()
    {
        List<object> list = new();
        
        list.Add(2);
        list.Add("ello");
        
        IntArray arrayOfInteger = new();
        arrayOfInteger.Add(0);
        list.Add(arrayOfInteger);
        
        Assert.Equal(2, list[0]);
        Assert.True(list.Contains(2));
        
        Assert.False(list.Contains(3));
        
        Assert.Equal("ello", list[1]);
        Assert.True(list.Contains("ello"));
        
        Assert.False(list.Contains("hello"));
        
        Assert.Equal(typeof(IntArray), list[2].GetType());
        Assert.True(list.Contains(arrayOfInteger));
        
        Assert.Equal(3, list.Count);
    }


    [Fact]
    public void RemoveStringElement()
    {
        List<object> list = new();
        list.Add("First element");
        list.Add(1);
        list.Add(2);
        list.Add(3);
        
        list.Remove("First element");
        Assert.False(list.Contains("First element"));
        Assert.Equal(1, list[0]);
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void RemoveObjectElement()
    {
        List<object> list = new();
        
        IntArray intArray = new();
        intArray.Add(10);
        intArray.Add(30);
        intArray.Add(20);

        SortedList<int> sortedList = new();
        sortedList.Add(60);
        sortedList.Add(40);
        sortedList.Add(50);
        
        list.Add(intArray);
        list.Add(sortedList);
        
        Assert.Equal(2, list.Count);
        
        list.Remove(intArray);
        Assert.Equal(1, list.Count);
        Assert.Equal(typeof(SortedList<int>), list[0].GetType());
    }

    [Fact]
    public void InsertNewElement()
    {
        List<object> list = new();
        list.Add(1);
        list.Add("1");

        IntArray intArray = new();
        intArray.Add(1);
        intArray.Add(2);
        intArray.Add(3);
        
        list.Add(intArray);
        list.Add(3.14);
        
        list.Insert(2, "element");
        
        Assert.Equal("element", list[2]);
        Assert.Equal(2, list.IndexOf("element"));
        Assert.Equal(5, list.Count);
    }

    [Fact]
    public void GetIndexOfNullElementInArray()
    {
        List<object> list = new();
        list.Add(null);
        Assert.Equal(0, list.IndexOf(null));
    }

    [Fact]
    public void GetElements()
    {
        var objectArray = new List<object> { 1, "ello", 2 };
        var elements = objectArray.GetEnumerator();
        elements.MoveNext();
        Assert.Equal(1, elements.Current);
        elements.MoveNext();
        Assert.Equal("ello", elements.Current);
        elements.MoveNext();
        Assert.Equal(2, elements.Current);
    }

    [Fact]
    public void GetElementsWhenThereIsNoElement()
    {
        var objectArray = new List<object> { };
        var elements = objectArray.GetEnumerator();
        elements.MoveNext();
        Assert.Null(elements.Current);
    }
    
    [Fact]
    public void RemoveExistingItemFromCollection()
    {
        List<string> list = new() { "abc", "xyz", "efg", "def", "opq" };
        var remove = ((ICollection<string>)list).Remove("xyz");
        Assert.True(remove);
        Assert.Equal("abc", list[0]);
        Assert.Equal("efg", list[1]);
        Assert.Equal("def", list[2]);
        Assert.Equal("opq", list[3]);
        Assert.Equal(4, list.Count);
    }
        
    [Fact]
    public void RemoveNonExistingItemFromCollection()
    {
        List<string> list = new() { "abc", "xyz", "efg", "def", "opq" };
        var remove = ((ICollection<string>)list).Remove("mno");
        Assert.False(remove);
        Assert.Equal("abc", list[0]);
        Assert.Equal("xyz", list[1]);
        Assert.Equal("efg", list[2]);
        Assert.Equal("def", list[3]);
        Assert.Equal("opq", list[4]);
        Assert.Equal(5, list.Count);
    }
}