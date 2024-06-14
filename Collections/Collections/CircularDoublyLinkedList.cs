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
        if (node == null)
        {
            throw new InvalidOperationException(nameof(node));
        }

        if (newNode == null)
        {
            throw new InvalidOperationException(nameof(newNode));
        }

        newNode.Next = node;
        newNode.Previous = node.Previous;
        node.Previous.Next = newNode;
        node.Previous = newNode;
        Count++;
    }

    public void AddBefore(Node<T> node, T value) => AddBefore(node, new Node<T>(value));

    public void AddAfter(Node<T> node, Node<T> newNode) => AddBefore(node.Next, newNode);

    public void AddAfter(Node<T> node, T value) => AddBefore(node.Next, new Node<T>(value));

    public void AddFirst(T value) => AddFirst(new Node<T>(value));

    public void AddFirst(Node<T> node) => AddAfter(sentinel, node);

    public void AddLast(Node<T> node) => AddBefore(sentinel, node);

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