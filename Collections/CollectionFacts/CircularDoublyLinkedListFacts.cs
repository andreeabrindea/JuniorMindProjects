using Collections;
using Xunit;

namespace CircularDoublyLinkedListFacts;

public class CircularDoublyLinkedListFacts
{

    [Fact]
    public void AddNewNodeAtTheEndOfList()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        
        Assert.Equal(new CircularDoublyLinkedList<int> { 1, 2, 3 }, list);
    }

    [Fact]
    public void ClearAddedNodes()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Clear();
        Assert.Equal(new CircularDoublyLinkedList<int> {}, list);
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
        Assert.Equal(new List<int> { 1, 3 }, list);
    }
    
    [Fact]
    public void RemoveNonExistingElementInList()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);

        Assert.False(list.Remove(5));
        Assert.Equal(new CircularDoublyLinkedList<int> {1, 2, 3}, list);
    }

    [Fact]
    public void AddNodeToTheFirstPositionInEmptyList()
    {
        CircularDoublyLinkedList<int> list = new();
        list.AddFirst(1);
        list.AddFirst(2);
        list.AddFirst(3);
 
        Assert.Equal(new CircularDoublyLinkedList<int> {3, 2, 1}, list);
    }
    
    [Fact]
    public void AddNodeToTheFirstPositionInNonEmptyList()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.AddFirst(9);

        Assert.Equal(new CircularDoublyLinkedList<int>{9, 1, 2, 3}, list);
    }

    [Fact]
    public void RemoveLastElementInNonEmptyList()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.AddFirst(9);

        Assert.True(list.RemoveLast());
        Assert.Equal(new CircularDoublyLinkedList<int>{9, 1, 2}, list);
    }

    [Fact]
    public void RemoveLastElementInEmptyList()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Remove(1);
        Assert.False(list.RemoveLast());
        Assert.Empty(list);
    }

    [Fact]
    public void RemoveFirstElementInNonEmptyList()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.AddFirst(9);

        Assert.True(list.RemoveFirst());
        Assert.Equal(new CircularDoublyLinkedList<int>{1, 2, 3}, list);
    }
    
    [Fact]
    public void RemoveFirstElementInEmptyList()
    {
        CircularDoublyLinkedList<int> list = new();
        Assert.False(list.RemoveFirst());
    }

    [Fact]
    public void AddNewNodeAfterAnExistingElement()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        list.AddAfter(2, 8);
        Assert.Equal(new CircularDoublyLinkedList<int> {1, 2, 8, 3, 4}, list);
    }
    
    [Fact]
    public void AddNewNodeAfterNonExistingElement()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        
        list.AddAfter(9, 8);
        Assert.Equal(new CircularDoublyLinkedList<int>{1, 2, 3, 4}, list);
    }

    [Fact]
    public void AddNewNodeInEmptyList()
    {
        CircularDoublyLinkedList<int> list = new();
        list.AddAfter(0, 8);
        Assert.Equal(new CircularDoublyLinkedList<int>{}, list);
    }
    
    [Fact]
    public void AddNewNodeBeforeAnExistingElement()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        
        list.AddBefore(3, 8);
        Assert.Equal(new CircularDoublyLinkedList<int>{1, 2, 8, 3, 4}, list);
    }

    [Fact]
    public void FindLastNonExistingElement()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        var node = list.FindLast(21);
        Assert.Null(node);
    }
    
    [Fact]
    public void FindNonExistingElement()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        var node = list.Find(21);
        Assert.Null(node);
    }
    
    [Fact]
    public void GetFirstNodeOfNonEmptyList()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        
        Assert.Equal(1, list.First.Data);
    }
    
    [Fact]
    public void GetFirstNodeOfEmptyList()
    {
        CircularDoublyLinkedList<int> list = new();
        Assert.Null(list.First);
    }
    
        
    [Fact]
    public void GetLastNodeOfEmptyList()
    {
        CircularDoublyLinkedList<int> list = new();
        Assert.Null(list.Last);
    }
    
    [Fact]
    public void GetLastNodeOfNonEmptyList()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        
        Assert.Equal(4, list.Last.Data);
    }
    
     [Fact]
    public void AddNewNodeAfterAnExistingOne()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        var previousNode = list.FindLast(2);
        var node = new Node<int>(9);
        list.AddAfter(previousNode, node);
        Assert.Equal(new CircularDoublyLinkedList<int>{1, 2, 9, 3, 4}, list);
    }

    [Fact]
    public void AddNewNodeAfterANonExistingOne()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        var previousNode = new Node<int>(11);
        var node = new Node<int>(9);
        list.AddAfter(previousNode, node);
        Assert.Equal(new CircularDoublyLinkedList<int>{1, 2, 3, 4}, list);
    }

    [Fact]
    public void AddNewNodeBeforeAnExistingOne()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        var nextNode = list.FindLast(2);
        var node = new Node<int>(9);
        list.AddBefore(nextNode, node);

        Assert.Equal(new CircularDoublyLinkedList<int>{1, 9, 2, 3, 4}, list);
    }

    [Fact]
    public void AddNewNodeBeforeANonExistingOne()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        var nextNode = new Node<int>(11);
        var node = new Node<int>(9);
        list.AddBefore(nextNode, node);

        Assert.Equal(new CircularDoublyLinkedList<int>{1, 2, 3, 4}, list);
    }

    [Fact]
    public void AddNodeToLastPosition()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        var newNode = new Node<int>(5);
        list.AddLast(newNode);
        Assert.Equal(new CircularDoublyLinkedList<int>{1, 2, 3, 4, 5}, list);
    }

    [Fact]
    public void AddNodeToFirstPosition()
    {
        CircularDoublyLinkedList<int> list = new();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        var newNode = new Node<int>(5);
        list.AddFirst(newNode);
        Assert.Equal(new CircularDoublyLinkedList<int>{5, 1, 2, 3, 4}, list);
    }
}