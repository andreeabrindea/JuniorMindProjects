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

    public void AddBefore(Node<T> node, Node<T> newNode)
    {
        ArgumentNullException.ThrowIfNull(node);
        ArgumentNullException.ThrowIfNull(newNode);

        if (!Contains(node.Data) && !node.Equals(sentinel))
        {
            throw new InvalidOperationException("node was not found");
        }

        newNode.Next = node;
        newNode.Previous = node.Previous;
        node.Previous.Next = newNode;
        node.Previous = newNode;
        Count++;
    }

    public Node<T> AddBefore(Node<T> node, T value)
    {
        var newNode = new Node<T>(value);
        AddBefore(node,  newNode);
        return newNode;
    }

    public void AddAfter(Node<T> node, Node<T> newNode)
    {
        ArgumentNullException.ThrowIfNull(node);
        AddBefore(node.Next, newNode);
    }

    public Node<T> AddAfter(Node<T> node, T value)
    {
        ArgumentNullException.ThrowIfNull(node);
        var newNode = new Node<T>(value);
        AddBefore(node.Next, newNode);
        return newNode;
    }

    public void AddFirst(T value) => AddFirst(new Node<T>(value));

    public void AddFirst(Node<T> node) => AddAfter(sentinel, node);

    public void AddLast(Node<T> node) => AddBefore(sentinel, node);

    public Node<T> AddLast(T item)
    {
        var newNode = new Node<T>(item);
        AddLast(newNode);
        return newNode;
    }

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
        ArgumentNullException.ThrowIfNull(array);

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

        var nodeToRemove = Find(item);
        if (nodeToRemove == null)
        {
            return false;
        }

        nodeToRemove.Previous.Next = nodeToRemove.Next;
        nodeToRemove.Next.Previous = nodeToRemove.Previous;
        Count--;
        return true;
    }

    public void Remove(Node<T> node)
    {
        ArgumentNullException.ThrowIfNull(node);
        Remove(node.Data);
    }

    public void RemoveLast()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("the list is empty.");
        }

        sentinel.Previous = sentinel.Previous.Previous;
        sentinel.Previous.Next = sentinel;
        Count--;
    }

    public void RemoveFirst()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("the list is empty.");
        }

        sentinel.Next = sentinel.Next.Next;
        sentinel.Next.Previous = sentinel.Next.Next.Previous;
        Count--;
    }

    public Node<T> Find(T data)
    {
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