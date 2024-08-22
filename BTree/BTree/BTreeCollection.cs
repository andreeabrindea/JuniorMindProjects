using System.Collections;

namespace BTree;

public class BTreeCollection<T> : IEnumerable<T>
    where T : IComparable<T>
{
    private readonly Node<T> root;

    private readonly int degree;

    public BTreeCollection(int degree = 3)
    {
        Count = 0;
        this.degree = degree;
        this.root = new Node<T>(this.degree, true);
    }

    public int Count { get; set; }

    public bool IsReadOnly { get; }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        Node<T> node = root;
        if (Count == 0)
        {
            node.AddKey(item);
            Count++;
            return;
        }

        if (node.KeyCount == node.ChildrenCount + 1)
        {
            node.AddKey(item);
            Count++;
        }
        else
        {
            Node<T> newNode = new(degree, true);
            newNode.AddKey(item);
            node.AddChild(newNode);
        }
    }

    public bool Search(Node<T> node, T item)
    {
        if (node.Keys.Contains(item))
        {
            return true;
        }

        if (item.CompareTo(node.LargestKey()) > 0)
        {
            Search(node.Children[node.ChildrenCount], item);
        }

        if (item.CompareTo(node.SmallestKey()) < 0)
        {
            Search(node.Children[0], item);
        }

        for (int i = 1; i < node.KeyCount; i++)
        {
            if (node.Keys[i - 1].CompareTo(item) < 0 && node.Keys[i].CompareTo(item) > 0)
            {
                Search(node.Children[i], item);
            }
        }

        return false;
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(T item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T item)
    {
        throw new NotImplementedException();
    }
}
