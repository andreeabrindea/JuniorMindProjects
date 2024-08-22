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
        this.ChildrenCount = 0;
        this.keys = new T[degree];
        this.children = new Node<T>[degree + 1];
        IsLeaf = isLeaf;
    }

    internal int KeyCount { get; private set; }

    internal int Degree { get; }

    internal bool IsLeaf { get; set; }

    internal int ChildrenCount { get; set; }

    internal Node<T>[] Children => children;

    internal T[] Keys => keys;

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

    internal void AddChild(Node<T> node)
    {
        if (ChildrenCount < Degree + 1)
        {
            children[ChildrenCount] = node;
            ChildrenCount++;
            node.SortKeys();
        }
        else
        {
            throw new InvalidOperationException("The node attained the maximum number of children.");
        }
    }

    internal T LargestKey()
    {
        T max = keys[0];
        for (int i = 1; i <= KeyCount; i++)
        {
            if (keys[i].CompareTo(max) > 0)
            {
                max = keys[i];
            }
        }

        return max;
    }

    internal T SmallestKey()
    {
        T min = keys[0];
        for (int i = 1; i <= KeyCount; i++)
        {
            if (keys[i].CompareTo(min) < 0)
            {
                min = keys[i];
            }
        }

        return min;
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