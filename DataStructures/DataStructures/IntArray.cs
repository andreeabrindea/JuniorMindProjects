namespace DataStructures;

public class IntArray
{
    private int[] arrayOfIntegers;
    private int count;

    public IntArray(int initialCapacity = 3)
    {
        arrayOfIntegers = new int[initialCapacity];
        count = 0;
    }

    public void Add(int element)
    {
        EnsureCapacity();

        arrayOfIntegers[count] = element;
        count++;
    }

    public int Count()
    {
        return count;
    }

    public int Element(int index)
    {
        if (index < 0 || index > count)
        {
            return -1;
        }

        return arrayOfIntegers[index];
    }

    public void SetElement(int index, int element)
    {
        if (index < 0 || index > count)
        {
            return;
        }

        arrayOfIntegers[index] = element;
    }

    public bool Contains(int element)
    {
        return IndexOf(element) > -1;
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
        if (index < 0 || index > count)
        {
            return;
        }

        EnsureCapacity();
        ShiftElements(index);
        arrayOfIntegers[index] = element;
        count++;
    }

    public void Clear()
    {
        Array.Clear(arrayOfIntegers, 0, arrayOfIntegers.Length);
        count = 0;
    }

    public void Remove(int element)
    {
        RemoveAt(IndexOf(element));
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index > count)
        {
            return;
        }

        ShiftElements(index);
        count--;
    }

    private void EnsureCapacity()
    {
        if (count < arrayOfIntegers.Length)
        {
            return;
        }

        int resizingValue = arrayOfIntegers.Length * 2;
        Array.Resize(ref arrayOfIntegers, resizingValue);
    }

    private void ShiftElements(int index)
    {
        for (int i = index + 1; i < count; i++)
        {
            arrayOfIntegers[i - 1] = arrayOfIntegers[i];
        }
    }
}
