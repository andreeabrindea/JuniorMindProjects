namespace DataStructures;

public class IntArray
{
    private int[] arrayOfIntegers;
    private int count;

    public IntArray(int initialCapacity = 3)
    {
        Array.Resize(ref arrayOfIntegers, initialCapacity);
        count = 0;
    }

    public void Add(int element)
    {
        VerifyCapacity();

        arrayOfIntegers[count] = element;
        count++;
    }

    public int Count()
    {
        return count;
    }

    public int Element(int index)
    {
        if (index < 0)
        {
            return -1;
        }

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
        if (index < 0)
        {
            return;
        }

        VerifyCapacity();

        for (int i = arrayOfIntegers.Length - 1; i > index; i--)
        {
            arrayOfIntegers[i] = arrayOfIntegers[i - 1];
        }

        arrayOfIntegers[index] = element;
        count++;
    }

    public void Clear()
    {
        Array.Clear(arrayOfIntegers, 0, arrayOfIntegers.Length);
    }

    public void Remove(int element)
    {
        int found = 0;
        for (int i = 0; i < count; i++)
        {
            if (arrayOfIntegers[i] == element)
            {
                found = 1;
                for (int j = i + 1; j < count; j++)
                {
                    arrayOfIntegers[j - 1] = arrayOfIntegers[j];
                }
            }
        }

        if (found != 1)
        {
            return;
        }

        count--;
    }

    public void RemoveAt(int index)
    {
        if (index < 0)
        {
            return;
        }

        for (int i = index + 1; i < count; i++)
        {
            arrayOfIntegers[i - 1] = arrayOfIntegers[i];
        }

        count--;
    }

    private void VerifyCapacity()
    {
        if (count < arrayOfIntegers.Length)
        {
            return;
        }

        int resizingValue = arrayOfIntegers.Length * 2;
        Array.Resize(ref arrayOfIntegers, resizingValue);
    }
}
