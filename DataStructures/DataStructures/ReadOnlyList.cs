using System.Collections;

namespace DataStructures;

public class ReadOnlyList<T> : IList<T>
{
    private readonly IList<T> list;

    public ReadOnlyList(IList<T> list)
    {
        this.list = list;
    }

    public int Count => list.Count;

    public bool IsReadOnly => true;

    public T this[int index]
    {
        get => list[index];
        set => throw new NotSupportedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        ThrowNotSupportedException();
    }

    public void Clear()
    {
        ThrowNotSupportedException();
    }

    public bool Contains(T item)
    {
        return this.list.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        this.list.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        ThrowNotSupportedException();
        return false;
    }

    public int IndexOf(T item)
    {
        return list.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        ThrowNotSupportedException();
    }

    public void RemoveAt(int index)
    {
        ThrowNotSupportedException();
    }

    private void ThrowNotSupportedException()
    {
        if (!IsReadOnly)
        {
            return;
        }

        throw new NotSupportedException();
    }
}