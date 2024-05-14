namespace DataStructures;

public class ObjectArray<T>
{
    private readonly int count;
    private T[] arrayOfObjects;

    public ObjectArray(int initialCapacity = 3)
    {
        this.arrayOfObjects = new T[initialCapacity];
        count = 0;
    }

    public int Count { get; private set; }

    public T this[int index]
    {
        get => arrayOfObjects[index];
        set => arrayOfObjects[index] = value;
    }

    public void Add(T element)
    {
        EnsureCapacity();

        arrayOfObjects[Count] = element;
        Count++;
    }

    public bool Contains(T element)
    {
        return IndexOf(element) > -1;
    }

    public int IndexOf(T element)
    {
        for (int i = 0; i < arrayOfObjects.Length; i++)
        {
            if (this[i] != null && this[i].Equals(element))
            {
                return i;
            }
        }

        return -1;
    }

    public void Insert(int index, T element)
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