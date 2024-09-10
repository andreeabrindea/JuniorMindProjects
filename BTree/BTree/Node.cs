namespace BTree;

public class Node<T>
    where T : IComparable<T>
{
    public Node(int degree, bool isLeaf)
    {
        Degree = degree;
        KeyCount = 0;
        ChildrenCount = 0;
        Keys = new(degree + 1);
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

    internal T LargestKey() => Keys[KeyCount - 1];

    internal T SmallestKey() => Keys[0];

    internal bool HasTooFewKeys()
    {
        int minimumNoOfKeys = Degree / 2;
        return KeyCount < minimumNoOfKeys;
    }

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
        SortChildren();
    }

    internal void Split(T item)
    {
        AddKey(item);
        int middle = KeyCount / 2;
        T middleKey = Keys[middle];
        Node<T> leftNode = new(Degree, true);
        for (int i = 0; i < middle; i++)
        {
            leftNode.AddKey(Keys[i]);
        }

        Node<T> rightNode = new(Degree, true);
        for (int i = middle + 1; i < KeyCount; i++)
        {
            rightNode.AddKey(Keys[i]);
        }

        ClearKeys();
        AddKey(middleKey);
        IsLeaf = false;
        ChildrenCount = 0;
        AddChild(leftNode);
        AddChild(rightNode);
    }

    internal void RemoveChild(Node<T> node)
    {
        Children.Remove(node);
        ChildrenCount--;
    }

    internal void RemoveKey(T item)
    {
        Keys.Remove(item);
        KeyCount--;
    }

    internal int FindPositionOfNodeInParent(Node<T> node)
    {
        for (int i = 0; i < ChildrenCount; i++)
        {
            if (Children[i].Equals(node))
            {
                return i;
            }
        }

        return -1;
    }

    internal void ClearKeys()
    {
        Keys.Clear();
        KeyCount = 0;
    }

    internal void ClearChildren()
    {
        Children.Clear();
        ChildrenCount = 0;
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

    private void SortChildren()
    {
        for (int i = 0; i < ChildrenCount; ++i)
        {
            bool swapped = false;

            for (int j = 0; j < ChildrenCount - i - 1; ++j)
            {
                if (Children[j].Keys[0].CompareTo(Children[j + 1].Keys[0]) > 0)
                {
                    (Children[j], Children[j + 1]) = (Children[j + 1], Children[j]);

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