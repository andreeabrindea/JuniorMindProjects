using System.Collections;

namespace DataStructures;

public class ReadOnlyList<T> : IList<T>
{
    private readonly IList<T> readonlyListArray;

    public ReadOnlyList(IList<T> readonlyListArray)
    {
        this.readonlyListArray = readonlyListArray;
    }

    public int Count => readonlyListArray.Count;

    public bool IsReadOnly => true;

    public T this[int index]
    {
        get => readonlyListArray[index];
        set => throw new NotSupportedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return readonlyListArray.GetEnumerator();
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
        return this.readonlyListArray.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        this.readonlyListArray.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        ThrowNotSupportedException();
        return false;
    }

    public int IndexOf(T item)
    {
        return readonlyListArray.IndexOf(item);
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