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

    [Fact]
    public void ListContainsIntegerElement()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
  
        Assert.True(list.Contains(1));
    }
    
    [Fact]
    public void ListDoesNotContainIntegerElement()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
  
        Assert.False(list.Contains(9));
    }

    [Fact]
    public void ListContainsStringValue()
    {
        CircularDoublyLinkedList<string> list = new();
        list.Add("a");
  
        Assert.True(list.Contains("a"));
    }
    
    [Fact]
    public void ListDoesNotContainStringValue()
    {
        CircularDoublyLinkedList<string> list = new();
        list.Add("a");
  
        Assert.False(list.Contains("b"));
    }

    [Fact]
    public void ListContainsObject()
    {
        CircularDoublyLinkedList<object> list = new();
        Node<int> node = new Node<int>(4);
        list.Add(node);
        
        Assert.True(list.Contains(node));
    }
    [Fact]
    public void ListDoesNotContainObject()
    {
        CircularDoublyLinkedList<object> list = new();
        Node<int> node = new Node<int>(4);
        Node<int> anotherNode = new Node<int>(4);
        list.Add(node);
        
        Assert.False(list.Contains(anotherNode));
    }

    [Fact]
    public void CopyToEmptyArrayOfIntegers()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);

        int[] array = new int[3];
        list.CopyTo(array, 0);
        Assert.Equal(1, array[0]);
        Assert.Equal(2, array[1]);
        Assert.Equal(3, array[2]);
    }
    
    [Fact]
    public void CopyToNonEmptyArrayOfIntegers()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);

        int[] array = new int[4];
        array[0] = 6;
        list.CopyTo(array, 1);
        Assert.Equal(6, array[0]);
        Assert.Equal(1, array[1]);
        Assert.Equal(2, array[2]);
        Assert.Equal(3, array[3]);
    }

    [Fact]
    public void CopyToEmptyArrayButInvalidIndex()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);

        int[] array = new int[3];
        Assert.Throws<ArgumentOutOfRangeException>(() => list.CopyTo(array, -1));
        Assert.Throws<ArgumentOutOfRangeException>(() => list.CopyTo(array, 9));
    }

    [Fact]
    public void RemoveExistingElementInList()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);

        Assert.True(list.Remove(2));
        Assert.Equal(1, list[0]);
        Assert.Equal(3, list[1]);
    }
    
    [Fact]
    public void RemoveNonExistingElementInList()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);

        Assert.False(list.Remove(5));
        Assert.Equal(1, list[0]);
        Assert.Equal(2, list[1]);
        Assert.Equal(3, list[2]);
    }

    [Fact]
    public void AddNodeToTheFirstPositionInEmptyList()
    {
        CircularDoublyLinkedList<int> list = new();
        list.AddFirst(1);
        list.AddFirst(2);
        list.AddFirst(3);
        Assert.Equal(1, list[2]);
        Assert.Equal(2, list[1]);
        Assert.Equal(3, list[0]);
    }
    
    [Fact]
    public void AddNodeToTheFirstPositionInNonEmptyList()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.AddFirst(9);
        Assert.Equal(9, list[0]);
        Assert.Equal(1, list[1]);
        Assert.Equal(2, list[2]);
        Assert.Equal(3, list[3]);
    }
    
}