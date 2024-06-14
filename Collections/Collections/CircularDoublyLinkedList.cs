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

    public Node<T> First => Count == 0 ? null : sentinel.Next;

    public Node<T> Last => Count == 0 ? null : sentinel.Previous;

    public IEnumerator<T> GetEnumerator()
    {
        for (var node = sentinel.Next; node != sentinel; node = node.Next)
        {
            yield return node.Data;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item) => AddLast(new Node<T>(item));

    public void Add(Node<T> node, Node<T> nextNode, Node<T> previousNode)
    {
        node.Next = nextNode;
        node.Previous = previousNode;
        previousNode.Next = node;
        nextNode.Previous = node;
        Count++;
    }

    public void AddLast(Node<T> node) => Add(node, sentinel, sentinel.Previous);

    public void AddBefore(Node<T> nextNode, Node<T> newNode) => Add(newNode, nextNode, nextNode.Previous);

    public void AddBefore(T data, T newData)
    {
        var nextNode = Find(data);
        if (nextNode == null)
        {
            throw new InvalidOperationException("Node " + nameof(nextNode) + " was not found");
        }

        AddBefore(nextNode, new Node<T>(newData));
    }

    public void AddAfter(Node<T> previousNode, Node<T> newNode) => Add(newNode, previousNode.Next, previousNode);

    public void AddAfter(T data, T newData)
    {
        var previousNode = Find(data);
        if (previousNode == null)
        {
            throw new InvalidOperationException("Node " + nameof(previousNode) + " was not found");
        }

        AddAfter(previousNode, new Node<T>(newData));
    }

    public void AddFirst(T item) => AddFirst(new Node<T>(item));

    public void AddFirst(Node<T> node) => AddBefore(sentinel.Next, node);

    public void Clear()
    {
        sentinel.Previous = sentinel;
        sentinel.Next = sentinel;
        Count = 0;
    }

    public bool Contains(T item)
    {
        return Find(item) != null;
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

        var node = sentinel.Next;
        for (int i = 0; i < Count; i++)
        {
            array[arrayIndex + i] = node.Data;
            node = node.Next;
        }
    }

    public bool Remove(T item)
    {
        if (!Contains(item))
        {
            return false;
        }

        for (var node = sentinel.Next; node != sentinel; node = node.Next)
        {
            if (node.Data.Equals(item))
            {
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;
                Count--;
                return true;
            }
        }

        return false;
    }

    public bool Remove(Node<T> node) => Remove(node.Data);

    public bool RemoveLast()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException();
        }

        sentinel.Previous = sentinel.Previous.Previous;
        sentinel.Previous.Next = sentinel;
        Count--;
        return true;
    }

    public bool RemoveFirst()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException();
        }

        sentinel.Next = sentinel.Next.Next;
        sentinel.Next.Previous = sentinel.Next.Next.Previous;
        Count--;
        return true;
    }

    public Node<T> Find(T data)
    {
        if (Count == 0)
        {
            return null;
        }

        for (var node = sentinel.Next; node != sentinel; node = node.Next)
        {
            if (node.Data.Equals(data))
            {
                return node;
            }
        }

        return null;
    }

    public Node<T> FindLast(T data)
    {
        if (Count == 0)
        {
            return null;
        }

        for (var node = sentinel.Previous; node != sentinel; node = node.Previous)
        {
            if (node.Data.Equals(data))
            {
                return node;
            }
        }

        return null;
    }
}
#pragma warning restore CA1710