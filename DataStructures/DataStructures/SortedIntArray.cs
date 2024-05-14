namespace DataStructures;

public class SortedIntArray : IntArray
{
    public override void Add(int element)
    {
        base.Add(element);
        BubbleSort();
    }

    public override void Insert(int index, int element)
    {
        base.Insert(index, element);
        BubbleSort();
    }

    private void BubbleSort()
    {
        for (int i = 0; i < Count; ++i)
        {
            bool swapped = false;

            for (int j = 0; j < Count - i - 1; ++j)
            {
                if (this[j] > this[j + 1])
                {
                    (this[j], this[j + 1]) = (this[j + 1], this[j]);

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