namespace DataStructures;

public class SortedIntArray : IntArray
{
    public override int this[int index]
    {
        get => arrayOfIntegers[index];
        set => arrayOfIntegers[index] = value;
    }

    public override void Add(int element)
    {
        base.Add(element);
        BubbleSort();
    }

    private void BubbleSort()
    {
        for (int i = 2; i < arrayOfIntegers.Count(); ++i)
        {
            bool swapped = false;

            for (int j = 2; j < arrayOfIntegers.Count() - i - 1; ++j)
            {
                if (arrayOfIntegers[j] > arrayOfIntegers[j + 1])
                {
                    (arrayOfIntegers[j], arrayOfIntegers[j + 1]) = (arrayOfIntegers[j + 1], arrayOfIntegers[j]);

                    swapped = true;
                }
            }

            if (!swapped)
            {
                break;
            }
        }
    }
}