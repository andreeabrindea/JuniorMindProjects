using Collections;
using Xunit;

namespace CircularDoublyLinkedListFacts;

public class CircularLinkedListFacts
{

    [Fact]
    public void AddNewNodeAtTheEndOfList()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        Assert.Equal(1, list[0]);
        Assert.Equal(2, list[1]);
        Assert.Equal(3, list[2]);
    }

    [Fact]
    public void ClearAddedNodes()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        Assert.Equal(1, list[0]);
        Assert.Equal(2, list[1]);
        Assert.Equal(3, list[2]);
        
        list.Clear();
        Assert.Equal(0, list[0]);
    }
}