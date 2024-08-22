namespace BTree;

public class Node<T>
    where T : IComparable<T>
{
    private readonly T[] keys;
    private readonly Node<T>[] children;

    public Node(int degree, bool isLeaf)
    {
        this.Degree = degree;
        this.KeyCount = 0;
        this.keys = new T[degree];
        this.children = new Node<T>[degree + 1];
        IsLeaf = isLeaf;
    }

    internal int KeyCount { get; private set; }

    internal int Degree { get; }

    internal bool IsLeaf { get; set; }

    internal void AddKey(T item)
    {
        if (KeyCount < Degree)
        {
            keys[KeyCount] = item;
            KeyCount++;
            SortKeys();
        }
        else
        {
            throw new InvalidOperationException("The node attained the maximum number of keys.");
        }
    }

    private void SortKeys()
    {
        for (int i = 0; i < keys.Length; ++i)
        {
            bool swapped = false;

            for (int j = 0; j < keys.Length - i - 1; ++j)
            {
                if (keys[j].CompareTo(keys[j + 1]) > 0)
                {
                    (keys[j], keys[j + 1]) = (keys[j + 1], keys[j]);

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