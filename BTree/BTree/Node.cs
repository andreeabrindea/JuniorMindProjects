namespace BTree;

public class Node<T>
    where T : IComparable<T>
{
    public Node(int degree, bool isLeaf)
    {
        Degree = degree;
        KeyCount = 0;
        ChildrenCount = 0;
        Keys = new(degree);
        Children = new(degree + 1);
        IsLeaf = isLeaf;
    }

    public int KeyCount { get; private set; }

    internal bool IsFull => Degree == KeyCount;

    internal int Degree { get; }

    internal bool IsLeaf { get; private set; }

    internal int ChildrenCount { get; private set; }

    internal List<Node<T>> Children { get; set; }

    internal List<T> Keys { get; set; }

    internal T LargestKey() => Keys[KeyCount];

    internal T SmallestKey() => Keys[0];

    internal void AddKey(T item)
    {
        Keys.Add(item);
        KeyCount++;
        SortKeys();
    }

    internal void AddChild(Node<T> node)
    {
        Children.Add(node);
        ChildrenCount++;
    }

    internal void Split(T item)
    {
        int middle = (KeyCount + 1) / 2;
        T middleKey = Keys[middle];
        RemoveKey(middleKey);
        AddKey(item);
        Node<T> leftNode = new(Degree, true);
        for (int i = 0; i < middle; i++)
        {
            leftNode.AddKey(Keys[i]);
        }

        Node<T> rightNode = new(Degree, true);
        for (int i = middle; i < KeyCount; i++)
        {
            rightNode.AddKey(Keys[i]);
        }

        KeyCount = 1;
        Keys[0] = middleKey;
        for (int i = 1; i < Keys.Count; i++)
        {
            Keys.RemoveAt(i);
        }

        IsLeaf = false;
        ChildrenCount = 0;
        AddChild(leftNode);
        AddChild(rightNode);
    }

    private void RemoveKey(T item)
    {
        for (int i = 1; i < KeyCount; i++)
        {
            if (Keys[i - 1].Equals(item))
            {
                Keys[i - 1] = Keys[i];
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
                if (Keys[j].CompareTo(Keys[j + 1]) > 0)
                {
                    (Keys[j], Keys[j + 1]) = (Keys[j + 1], Keys[j]);

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