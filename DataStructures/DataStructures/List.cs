#pragma warning disable CA1710

using System.Collections;

namespace DataStructures;

public class List<T> : IList<T>
{
    private readonly int count;
    private T[] arrayOfObjects;

    public List(int initialCapacity = 3)
    {
        arrayOfObjects = new T[initialCapacity];
        count = 0;
    }

    public int Count { get; private set; }

    public bool IsReadOnly => false;

    public virtual T this[int index]
    {
        get => arrayOfObjects[index];
        set => arrayOfObjects[index] = value;
    }

    bool ICollection<T>.Remove(T item)
    {
        if (IndexOf(item) < 0)
        {
            return false;
        }

        Remove(item);
        return IndexOf(item) < 0;
    }

    public virtual void Add(T element)
    {
        EnsureCapacity();

        arrayOfObjects[Count] = element;
        Count++;
    }

    public void Clear()
    {
        Array.Clear(arrayOfObjects, 0, arrayOfObjects.Length);
        Count = 0;
    }

    public bool Contains(T element)
    {
        return IndexOf(element) > -1;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        for (int i = 0; i < Count; i++)
        {
            array[arrayIndex + i] = arrayOfObjects[i];
        }
    }

    public int IndexOf(T element)
    {
        for (int i = 0; i < arrayOfObjects.Length; i++)
        {
            var isNull = arrayOfObjects[i] == null && element == null;
            if (isNull || this[i]?.Equals(element) == true)
            {
                return i;
            }
        }

        return -1;
    }

    public virtual void Insert(int index, T element)
    {
        if (index < 0 || index > Count)
        {
            return;
        }

        EnsureCapacity();
        ShiftElementsToRight(index);
        this[index] = element;
        Count++;
    }

    public void Remove(T element)
    {
        RemoveAt(IndexOf(element));
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index > Count)
        {
            return;
        }

        ShiftElementsToLeft(index);
        Count--;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return arrayOfObjects[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private void EnsureCapacity()
    {
        if (Count < arrayOfObjects.Length)
        {
            return;
        }

        int resizingValue = arrayOfObjects.Length * 2;
        Array.Resize(ref arrayOfObjects, resizingValue);
    }

    private void ShiftElementsToLeft(int index)
    {
        for (int i = index + 1; i < Count; i++)
        {
            this[i - 1] = this[i];
        }
    }

    private void ShiftElementsToRight(int index)
    {
        for (int i = Count; i > index; i--)
        {
            this[i] = this[i - 1];
        }
    }
}
#pragma warning restore CA1710