namespace DataStructures;

public class SortedList<T> : List<T>
    where T : IComparable<T>
{
    public virtual T this[int index]
    {
        get => base[index];
        set
        {
            base[index] = value;
            BubbleSort();
        }
    }

    public override void Add(T element)
    {
        base.Add(element);
        BubbleSort();
    }

    public override void Insert(int index, T element)
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
                if (this[j].CompareTo(this[j + 1]) > 0)
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