﻿#pragma warning disable CA1710
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

    public Node<T> First => sentinel.Next;

    public Node<T> Last => sentinel.Previous;

    public IEnumerator<T> GetEnumerator()
    {
        var node = sentinel.Next;
        for (int i = 0; i < Count; i++)
        {
            yield return node.Data;
            node = node.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        var node = new Node<T>(item);
        var lastNode = sentinel.Previous;

        node.Next = sentinel;
        node.Previous = lastNode;
        lastNode.Next = node;
        sentinel.Previous = node;

        Count++;
    }

    public void AddFirst(T item)
    {
        var node = new Node<T>(item);
        var firstNode = sentinel.Next;

        node.Previous = sentinel;
        sentinel.Next = node;
        node.Next = firstNode;
        firstNode.Previous = node;
        Count++;
    }

    public void AddAfter(T data, T newData)
    {
        var newNode = new Node<T>(newData);

        if (Count == 0)
        {
            Add(newData);
            return;
        }

        var node = sentinel.Next;
        for (int i = 0; i < Count; i++)
        {
            if (node.Data.Equals(data))
            {
                newNode.Previous = node;
                newNode.Next = node.Next;
                node.Next.Previous = newNode;
                node.Next = newNode;
                Count++;
                return;
            }

            node = node.Next;
        }
    }

    public void AddBefore(T data, T newData)
    {
        var newNode = new Node<T>(newData);
        if (Count == 0)
        {
            Add(newData);
            return;
        }

        var node = sentinel.Next;
        for (int i = 0; i < Count; i++)
        {
            if (node.Data.Equals(data))
            {
                newNode.Next = node;
                newNode.Previous = node.Previous;
                node.Previous.Next = newNode;
                node.Previous = newNode;
                Count++;
                return;
            }

            node = node.Next;
        }
    }

    public void Clear()
    {
        sentinel.Previous = sentinel;
        sentinel.Next = sentinel;
        Count = 0;
    }

    public bool Contains(T item)
    {
        var node = sentinel.Next;
        for (int i = 0; i < Count; i++)
        {
            if (node.Data.Equals(item))
            {
                return true;
            }

            node = node.Next;
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

        var node = sentinel.Next;
        for (int i = 0; i < Count; i++)
        {
            array[arrayIndex + i] = node.Data;
            node = node.Next;
        }
    }

    public bool Remove(T item)
    {
        var node = sentinel.Next;
        for (int i = 0; i < Count; i++)
        {
            if (node.Data.Equals(item))
            {
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;
                Count--;
                return true;
            }

            node = node.Next;
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

    public bool RemoveFirst()
    {
        if (Count <= 0)
        {
            return false;
        }

        sentinel.Next = sentinel.Next.Next;
        sentinel.Next.Previous = sentinel.Next.Next.Previous;
        Count--;
        return true;
    }

    public Node<T> Find(T data)
    {
        var node = sentinel.Next;
        for (int i = 0; i < Count; i++)
        {
            if (node.Data.Equals(data))
            {
                return node;
            }

            node = node.Next;
        }

        return null;
    }

    public Node<T> FindLast(T data)
    {
        var node = sentinel.Next;
        Node<T> lastFound = null;
        for (int i = 0; i < Count; i++)
        {
            if (node.Data.Equals(data))
            {
                lastFound = node;
            }

            node = node.Next;
        }

        return lastFound;
    }

    public override string ToString()
    {
        var node = sentinel.Next;
        string representation = "";

        for (int i = 0; i < Count; i++)
        {
            representation += node.Data + " ";

            node = node.Next;
        }

        return representation;
    }
}
#pragma warning restore CA1710