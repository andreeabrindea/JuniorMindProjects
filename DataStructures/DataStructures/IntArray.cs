﻿namespace DataStructures;

public class IntArray
{
    private readonly int count;
    private int[] arrayOfIntegers;

    public IntArray(int initialCapacity = 3)
    {
        arrayOfIntegers = new int[initialCapacity];
        count = 0;
    }

    public int Count { get; private set; }

    public void Add(int element)
    {
        EnsureCapacity();

        arrayOfIntegers[Count] = element;
        Count++;
    }

    public int Element(int index)
    {
        if (index < 0 || index > Count)
        {
            return -1;
        }

        return arrayOfIntegers[index];
    }

    public void SetElement(int index, int element)
    {
        if (index < 0 || index > Count)
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
        if (index < 0 || index > Count)
        {
            return;
        }

        EnsureCapacity();
        ShiftElementsToRight(index);
        arrayOfIntegers[index] = element;
        Count++;
    }

    public void Clear()
    {
        Array.Clear(arrayOfIntegers, 0, arrayOfIntegers.Length);
        Count = 0;
    }

    public void Remove(int element)
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
        if (Count < arrayOfIntegers.Length)
        {
            return;
        }

        int resizingValue = arrayOfIntegers.Length * 2;
        Array.Resize(ref arrayOfIntegers, resizingValue);
    }

    private void ShiftElementsToLeft(int index)
    {
        for (int i = index + 1; i < Count; i++)
        {
            arrayOfIntegers[i - 1] = arrayOfIntegers[i];
        }
    }

    private void ShiftElementsToRight(int index)
    {
        for (int i = Count; i > index; i--)
        {
            arrayOfIntegers[i] = arrayOfIntegers[i - 1];
        }
    }
}
