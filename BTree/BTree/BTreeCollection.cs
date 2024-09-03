using System.Collections;

namespace BTree;

public class BTreeCollection<T> : IEnumerable<T>
    where T : IComparable<T>
{
    private readonly Node<T> root;

    public BTreeCollection(int degree = 3)
    {
        Count = 0;
        this.root = new Node<T>(degree, true);
    }

    public int Count { get; set; }

    public bool IsReadOnly => false;

    public IEnumerator<T> GetEnumerator() => GetKeys(root).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Node<T> Search(T item) => Search(root, item);

    public void Add(T item)
    {
        if (Contains(item))
        {
            return;
        }

        Add(root, item, root);
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
            if (item.CompareTo(node.Keys[i - 1]) > 0 && item.CompareTo(node.Keys[i]) < 0)
            {
                return Search(node.Children[i], item);
            }
        }

        return null;
    }

    private void Add(Node<T> node, T item,  Node<T> parent)
    {
        if (node.ChildrenCount == 0)
        {
            Count++;
            if (!node.IsFull)
            {
                node.AddKey(item);
                return;
            }

            node.Split(item);
            if (node != root)
            {
                MergeNodeWithParent(node, parent);
                return;
            }

            return;
        }

        if (item.CompareTo(node.SmallestKey()) < 0)
        {
            Add(node.Children[0], item, node);
            return;
        }

        if (item.CompareTo(node.LargestKey()) > 0)
        {
            Add(node.Children[node.ChildrenCount - 1], item, node);
            return;
        }

        for (int i = 1; i < node.KeyCount; i++)
        {
            if (node.Keys[i - 1].CompareTo(item) < 0 && node.Keys[i].CompareTo(item) > 0)
            {
                Add(node.Children[i], item, node);
                return;
            }
        }
    }

    private void MergeNodeWithParent(Node<T> node, Node<T> parent)
    {
        for (int i = 0; i < node.KeyCount; i++)
        {
            parent.AddKey(node.Keys[i]);
        }

        for (int i = 0; i < node.ChildrenCount; i++)
        {
            parent.AddChild(node.Children[i]);
        }

        parent.RemoveChild(node);
    }

    private IEnumerable<T> GetKeys(Node<T> node)
    {
        if (node == null)
        {
            yield break;
        }

        for (int i = 0; i < node.KeyCount; i++)
        {
            yield return node.Keys[i];
        }

        if (!node.IsLeaf)
        {
            for (int i = 0; i < node.ChildrenCount; i++)
            {
                foreach (var key in GetKeys(node.Children[i]))
                {
                    yield return key;
                }
            }
        }
    }
}
