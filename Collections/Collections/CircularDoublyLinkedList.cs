#pragma warning disable CA1710
using System.Collections;

namespace Collections;

public class CircularDoublyLinkedList<T> : ICollection<T>
{
    private readonly Node<T> sentinel;
    private int count;

    public CircularDoublyLinkedList()
    {
        sentinel = new Node<T>();
        sentinel.Next = sentinel;
        sentinel.Previous = sentinel;
        count = 0;
    }

    public int Count => count;

    public bool IsReadOnly => false;

    public T this[int index]
    {
        get
        {
            var currentNode = sentinel.Next;
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }

            return currentNode.Data;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var currentNode = sentinel.Next;
        for (int i = 0; i < count; i++)
        {
            yield return currentNode.Data;
            currentNode = currentNode.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        var newNode = new Node<T>(item);
        var lastNode = sentinel.Previous;

        newNode.Next = sentinel;
        newNode.Previous = lastNode;
        lastNode.Next = newNode;
        sentinel.Previous = newNode;

        count++;
    }

    public void Clear()
    {
        sentinel.Previous = sentinel;
        sentinel.Next = sentinel;
        count = 0;
    }

    public bool Contains(T item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T item)
    {
        throw new NotImplementedException();
    }
}
#pragma warning restore CA1710