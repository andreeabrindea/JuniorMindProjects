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
        Count = 0;
        for (int i = 0; i < root.ChildrenCount; i++)
        {
            root.RemoveChild(root.Children[i]);
        }
    }

    public bool Contains(T item) => Search(item) != null;

    public void CopyTo(T[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);

        if (arrayIndex < 0 || arrayIndex > array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        if (array.Length - arrayIndex > Count)
        {
            throw new ArgumentException("not enough space to copy", nameof(array));
        }

        if (Count == 0)
        {
            return;
        }

        foreach (var key in this)
        {
            array[arrayIndex] = key;
            arrayIndex++;
        }
    }

    public bool Remove(T item)
    {
        if (!Contains(item))
        {
            return false;
        }

        Node<T> node = Search(item);
        node.RemoveKey(item);

        if (!node.HasTooFewKeys() && node.IsLeaf)
        {
            return true;
        }

        return RemoveKeyFromLeafNodeWithInsufficientNoOfKeys(root, item, root);
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

    private bool RemoveKeyFromLeafNodeWithInsufficientNoOfKeys(Node<T> node, T item, Node<T> parent)
    {
        if (node.Keys.Contains(item) && node.IsLeaf)
        {
            return MergeNodes(node, parent);
        }

        if (item.CompareTo(node.SmallestKey()) < 0)
        {
            return RemoveKeyFromLeafNodeWithInsufficientNoOfKeys(node.Children[0], item, node);
        }

        if (item.CompareTo(node.LargestKey()) > 0)
        {
            return RemoveKeyFromLeafNodeWithInsufficientNoOfKeys(node.Children[node.ChildrenCount - 1], item, node);
        }

        for (int i = 1; i < node.KeyCount; i++)
        {
            if (item.CompareTo(node.Keys[i - 1]) > 0 && item.CompareTo(node.Keys[i]) < 0)
            {
                return RemoveKeyFromLeafNodeWithInsufficientNoOfKeys(node.Children[i], item, node);
            }
        }

        return false;
    }

    private bool MergeNodes(Node<T> node, Node<T> parent)
    {
        int indexOfNode = parent.FindPositionOfNodeInParent(node);
        var sibling = FindTheSiblingWithSufficientKeys(indexOfNode, parent, out string origin);
        if (sibling == null)
        {
            MergeNodesWhenSiblingsDoNotHaveEnoughKeys(indexOfNode, node, parent);
            return true;
        }

        var keyFromSibling = origin == "right" ? sibling.SmallestKey() : sibling.LargestKey();
        var separatorKey = origin == "right" ? parent.Keys[indexOfNode - 1] : parent.Keys[indexOfNode];
        parent.RemoveKey(separatorKey);
        parent.AddKey(keyFromSibling);
        sibling.RemoveKey(keyFromSibling);
        sibling.AddKey(separatorKey);
        return true;
    }

    private Node<T> FindTheSiblingWithSufficientKeys(int indexOfNode, Node<T> parent, out string origin)
    {
        if (parent.ChildrenCount > indexOfNode + 1)
        {
            var rightSibling = parent.Children[indexOfNode + 1];
            if (!rightSibling.HasTooFewKeys())
            {
                origin = "right";
                return rightSibling;
            }
        }

        if (indexOfNode < 1)
        {
            origin = "none";
            return null;
        }

        var leftSibling = parent.Children[indexOfNode - 1];
        origin = "left";
        return !leftSibling.HasTooFewKeys() ? leftSibling : null;
    }

    private void MergeNodesWhenSiblingsDoNotHaveEnoughKeys(int indexOfNode, Node<T> node, Node<T> parent)
    {
        var separatorKey = parent.Keys[indexOfNode - 1];
        var sibling = parent.Children[indexOfNode - 1];
        for (int i = 0; i < node.KeyCount; i++)
        {
            sibling.AddKey(node.Keys[i]);
        }

        sibling.AddKey(separatorKey);
        parent.RemoveKey(separatorKey);
    }
}
