namespace DataStructures;

public class IntArray
{
    private readonly int capacity = 3;
    private int[] arrayOfIntegers;

    public IntArray()
    {
        arrayOfIntegers = new int[capacity];
    }

    public void Add(int element)
{
    if (capacity == arrayOfIntegers.Length)
    {
       ResizeArray();
    }

    arrayOfIntegers[^1] = element;
}

    public int Count()
    {
        return arrayOfIntegers.Length;
    }

    public int Element(int index)
    {
        return arrayOfIntegers[index];
    }

    public void SetElement(int index, int element)
    {
        arrayOfIntegers[index] = element;
    }

    public bool Contains(int element)
    {
        foreach (var integer in arrayOfIntegers)
        {
            if (integer == element)
            {
                return true;
            }
        }

        return false;
    }

    public int IndexOf(int element)
    {
        for (int i = 0; i < arrayOfIntegers.Length; i++)
        {
            if (arrayOfIntegers[i] == element)
            {
                return i;
            }
        }

        return -1;
    }

    public void Insert(int index, int element)
    {
        if (capacity == arrayOfIntegers.Length)
        {
            ResizeArray();
        }

        for (int i = arrayOfIntegers.Length - 1; i > index; i--)
        {
            arrayOfIntegers[i] = arrayOfIntegers[i - 1];
        }

        arrayOfIntegers[index] = element;
    }

    public void Clear()
    {
        Array.Clear(arrayOfIntegers, 0, arrayOfIntegers.Length);
    }

    public void Remove(int element)
    {
        for (int i = 0; i < arrayOfIntegers.Length; i++)
        {
            if (arrayOfIntegers[i] == element)
            {
                for (int j = i + 1; j < arrayOfIntegers.Length; j++)
                {
                    arrayOfIntegers[j] = arrayOfIntegers[j - 1];
                }
            }
        }
    }

    public void RemoveAt(int index)
    {
        arrayOfIntegers[index] = 0;
    }

    private void ResizeArray()
    {
        int resizingValue = capacity * 2;
        Array.Resize(ref arrayOfIntegers, resizingValue);
    }
}
