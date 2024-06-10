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
}