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
            if (this[i]!.Equals(element))
            {
                return i;
            }
        }

        return -1;
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
}