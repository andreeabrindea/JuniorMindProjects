using System.Collections;

namespace BTree;

public class BTreeCollection<T> : IEnumerable<T>
    where T : IComparable<T>
{
    private Node<T> root;

    public BTreeCollection(int degree = 3)
    {
        Count = 0;
        this.root = new Node<T>(degree, true);
    }

    public int Count { get; set; }

    public bool IsReadOnly => false;

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Node<T> Search(T item) => Search(root, item);

    public void Add(T item) => Add(root, item, root);

    public void Add(Node<T> node, T item,  Node<T> parent)
    {
        if (node.ChildrenCount == 0)
        {
            if (!node.IsFull)
            {
                node.AddKey(item);
                return;
            }

            node.Split(item);
            if (node != root)
            {
                parent.AddChild(node);
                return;
            }

            root = node;
            return;
        }

        if (item.CompareTo(node.SmallestKey()) < 0)
        {
            Add(node.Children[0], item, node);
        }

        if (item.CompareTo(node.LargestKey()) > 0)
        {
            Add(node.Children[node.ChildrenCount - 1], item, node);
        }

        for (int i = 1; i < node.KeyCount; i++)
        {
            if (node.Keys[i - 1].CompareTo(item) < 0 && node.Keys[i].CompareTo(item) > 0)
            {
                Add(node.Children[i], item, node);
            }
        }
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(T item) => Search(item) != null;

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T item)
    {
        throw new NotImplementedException();
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node.Keys.Contains(item))
        {
            return node;
        }

        if (node.IsLeaf)
        {
            return null;
        }

        if (item.CompareTo(node.SmallestKey()) < 0)
        {
            return Search(node.Children[0], item);
        }

        if (item.CompareTo(node.LargestKey()) > 0)
        {
            return Search(node.Children[node.ChildrenCount - 1], item);
        }

        for (int i = 1; i < node.KeyCount; i++)
        {
            if (node.Keys[i - 1].CompareTo(item) < 0 && node.Keys[i].CompareTo(item) > 0)
            {
                return Search(node.Children[i], item);
            }
        }

        return null;
    }
}
