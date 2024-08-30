namespace BTree;

public class Node<T>
    where T : IComparable<T>
{
    private readonly T[] keys;
    private readonly Node<T>[] children;

    public Node(int degree, bool isLeaf)
    {
        Degree = degree;
        KeyCount = 0;
        ChildrenCount = 0;
        keys = new T[degree];
        children = new Node<T>[degree + 1];
        IsLeaf = isLeaf;
    }

    public int KeyCount { get; private set; }

    internal bool IsFull => Degree == KeyCount;

    internal int Degree { get; }

    internal bool IsLeaf { get; set; }

    internal int ChildrenCount { get; set; }

    internal Node<T>[] Children => children;

    internal T[] Keys => keys;

    internal T LargestKey() => keys[KeyCount];

    internal T SmallestKey() => keys[0];

    internal void AddKey(T item)
    {
        keys[KeyCount] = item;
        KeyCount++;
        SortKeys();
    }

    internal void AddChild(Node<T> node)
    {
        children[ChildrenCount] = node;
        ChildrenCount++;
    }

    internal void Split(T item)
    {
        int middle = (KeyCount + 1) / 2;
        T middleKey = keys[middle];
        RemoveKey(middleKey);
        AddKey(item);
        Node<T> leftNode = new(Degree, true);
        for (int i = 0; i < middle; i++)
        {
            leftNode.AddKey(keys[i]);
        }

        Node<T> rightNode = new(Degree, true);
        for (int i = middle; i < KeyCount; i++)
        {
            rightNode.AddKey(keys[i]);
        }

        KeyCount = 1;
        keys[0] = middleKey;
        Array.Fill(keys, default, 1, Degree - 1);

        IsLeaf = false;
        AddChild(leftNode);
        AddChild(rightNode);
    }

    internal void RemoveKey(T item)
    {
        for (int i = 1; i < KeyCount; i++)
        {
            if (keys[i - 1].Equals(item))
            {
                keys[i - 1] = keys[i];
            }
        }

        KeyCount--;
    }

    private void SortKeys()
    {
        for (int i = 0; i < KeyCount; ++i)
        {
            bool swapped = false;

            for (int j = 0; j < KeyCount - i - 1; ++j)
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