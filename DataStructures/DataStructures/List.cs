#pragma warning disable CA1710

using System.Collections;

namespace DataStructures;

public class List<T> : IEnumerable
{
    private readonly int count;
    private T[] arrayOfObjects;

    public List(int initialCapacity = 3)
    {
        arrayOfObjects = new T[initialCapacity];
        count = 0;
    }

    public int Count { get; private set; }

    public T this[int index]
    {
        get => arrayOfObjects[index];
        set => arrayOfObjects[index] = value;
    }

    public virtual void Add(T element)
    {
        EnsureCapacity();

        arrayOfObjects[Count] = element;
        Count++;
    }

    public bool Contains(T element)
    {
        return IndexOf(element) > -1;
    }

    public int IndexOf(object element)
    {
        for (int i = 0; i < arrayOfObjects.Length; i++)
        {
            if ((arrayOfObjects[i] == null && element == null) || (this[i]?.Equals(element) == true))
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

    public void Remove(object element)
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

    public IEnumerable GetElements()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return arrayOfObjects[i];
        }
    }

    public IEnumerator GetEnumerator()
    {
        return new ListEnumerator<T>(this);
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