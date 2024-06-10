#pragma warning disable CA1710
using System.Collections;

namespace Collections;

public class CircularDoublyLinkedList<T> : ICollection<T>
{
    private readonly Node<T> sentinel;

    public CircularDoublyLinkedList()
    {
        sentinel = new Node<T>();
        sentinel.Next = sentinel;
        sentinel.Previous = sentinel;
        Count = 0;
    }

    public int Count { get; private set; }

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
        for (int i = 0; i < Count; i++)
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

        Count++;
    }

    public void AddFirst(T item)
    {
        var newNode = new Node<T>(item);
        var firstNode = sentinel.Next;

        newNode.Previous = sentinel;
        sentinel.Next = newNode;
        newNode.Next = firstNode;
        firstNode.Previous = newNode;
        Count++;
    }

    public void Clear()
    {
        sentinel.Previous = sentinel;
        sentinel.Next = sentinel;
        Count = 0;
    }

    public bool Contains(T item)
    {
        var currentNode = sentinel.Next;
        for (int i = 0; i < Count; i++)
        {
            if (currentNode.Data.Equals(item))
            {
                return true;
            }

            currentNode = currentNode.Next;
        }

        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (arrayIndex < 0 || arrayIndex > array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        if (array.Length - arrayIndex > Count)
        {
            throw new ArgumentException("not enough space to copy", nameof(array));
        }

        var currentNode = sentinel.Next;
        for (int i = 0; i < Count; i++)
        {
            array[arrayIndex + i] = currentNode.Data;
            currentNode = currentNode.Next;
        }
    }

    public bool Remove(T item)
    {
        var currentNode = sentinel.Next;
        for (int i = 0; i < Count; i++)
        {
            if (currentNode.Data.Equals(item))
            {
                currentNode.Previous.Next = currentNode.Next;
                currentNode.Next.Previous = currentNode.Previous;
                Count--;
                return true;
            }

            currentNode = currentNode.Next;
        }

        return false;
    }

    public bool RemoveLast()
    {
        if (Count <= 0)
        {
            return false;
        }

        sentinel.Previous = sentinel.Previous.Previous;
        sentinel.Previous.Next = sentinel;
        Count--;
        return true;
    }
}
#pragma warning restore CA1710