using System.Collections;

namespace DataStructures;

public class ReadOnlyList<T> : IList<T>
{
    private readonly List<T> array;

    public ReadOnlyList(List<T> array)
    {
        this.array = array;
    }

    public int Count => array.Count;

    public bool IsReadOnly => true;

    public T this[int index]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return array[i];
        }
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
        return this.array.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        this.array.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        ThrowNotSupportedException();
        return false;
    }

    public int IndexOf(T item)
    {
        return array.IndexOf(item);
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