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

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public Node<T> Search(T item) => Search(root, item);

    public Node<T> SearchNonRecursive(T item)
    {
        Stack<Node<T>> stack = new();
        stack.Push(root);
        while (stack.Count > 0)
        {
            Node<T> node = stack.Pop();
            if (node.Keys.Contains(item))
            {
                return node;
            }

            if (node.IsLeaf)
            {
                continue;
            }

            if (item.CompareTo(node.SmallestKey()) < 0)
            {
                stack.Push(node.Children[0]);
            }
            else if (item.CompareTo(node.LargestKey()) > 0)
            {
                stack.Push(node.Children[node.ChildrenCount - 1]);
            }
            else
            {
                int index = GetIndexOfItemBetweenSeparators(item, node);
                stack.Push(node.Children[index]);
            }
        }

        return null;
    }

    public void Add(T item) => Add(root, item, null);

    public void AddNonRecursive(T item)
    {
        Stack<(Node<T>, Node<T>)> stack = new();
        stack.Push((root, null));
        while (stack.Count > 0)
        {
            var (node, parent) = stack.Pop();
            if (node.Keys.Contains(item))
            {
                return;
            }

            if (node.ChildrenCount == 0)
            {
                AddItemIntoLeafNode(node, item, parent);
            }

            if (node.IsLeaf)
            {
                continue;
            }

            if (item.CompareTo(node.SmallestKey()) < 0)
            {
                stack.Push((node.Children[0], node));
            }
            else if (item.CompareTo(node.LargestKey()) > 0)
            {
                stack.Push((node.Children[node.ChildrenCount - 1], node));
            }
            else
            {
                int index = GetIndexOfItemBetweenSeparators(item, node);
                stack.Push((node.Children[index], node));
            }
        }
    }

    public void Clear()
    {
        Count = 0;
        root.ClearKeys();
        root.ClearChildren();
    }

    public bool Contains(T item) => Search(item) != null;

    public bool ContainsNonRecursive(T item) => SearchNonRecursive(item) != null;

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

    public void Remove(T item)
    {
        Node<T> node = Search(item);
        if (node == null)
        {
            return;
        }

        if (!node.HasTooFewKeys() && node.IsLeaf || Count == 0)
        {
            node.RemoveKey(item);
            Count--;
            return;
        }

        if (!node.IsLeaf)
        {
            node.RemoveKey(item);
            Count--;
            RemoveKeyFromInternalNode(node);
            return;
        }

        RemoveKeyFromLeafNodeWithInsufficientNoOfKeys(root, item, root);
    }

    public void RemoveNonRecursive(T item)
    {
        Node<T> node = SearchNonRecursive(item);
        if (node == null)
        {
            return;
        }

        if (!node.HasTooFewKeys() && node.IsLeaf || Count == 0)
        {
            node.RemoveKey(item);
            Count--;
            return;
        }

        if (!node.IsLeaf)
        {
            node.RemoveKey(item);
            Count--;
            RemoveKeyFromInternalNode(node);
            return;
        }

        RemoveKeyFromLeafNodeWithInsufficientNoOfKeysNonRecursive(item);
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
        if (node.Keys.Contains(item))
        {
            return;
        }

        if (node.ChildrenCount == 0)
        {
            AddItemIntoLeafNode(node, item, parent);
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

    private void AddItemIntoLeafNode(Node<T> node, T item, Node<T> parent)
    {
        Count++;
        if (!node.IsFull)
        {
            node.AddKey(item);
            return;
        }

        node.Split(item);
        if (node == root)
        {
            return;
        }

        MergeNodeWithParent(node, parent);
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
            node.RemoveKey(item);
            Count--;
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

    private void RemoveKeyFromLeafNodeWithInsufficientNoOfKeysNonRecursive(T item)
    {
        Stack<(Node<T>, Node<T>)> stack = new();
        stack.Push((root, null));
        while (stack.Count > 0)
        {
            var (node, parent) = stack.Pop();
            if (node.Keys.Contains(item) && node.IsLeaf)
            {
                node.RemoveKey(item);
                Count--;
                MergeNodes(node, parent);
            }

            if (node.IsLeaf)
            {
                continue;
            }

            if (item.CompareTo(node.SmallestKey()) < 0)
            {
                stack.Push((node.Children[0], node));
            }
            else if (item.CompareTo(node.LargestKey()) > 0)
            {
                stack.Push((node.Children[node.ChildrenCount - 1], node));
            }
            else
            {
                int index = GetIndexOfItemBetweenSeparators(item, node);
                stack.Push((node.Children[index], node));
            }
        }
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
        T separatorKey = parent.Keys[indexOfNode - 1];
        Node<T> sibling = parent.Children[indexOfNode - 1];
        for (int i = 0; i < node.KeyCount; i++)
        {
            sibling.AddKey(node.Keys[i]);
        }

        sibling.AddKey(separatorKey);
        parent.RemoveKey(separatorKey);
    }

    private void RemoveKeyFromInternalNode(Node<T> node)
    {
        Node<T> firstChild = node.Children[0];
        T biggestKeyFromLeftSubtree = node.Children[0].LargestKey();
        node.AddKey(biggestKeyFromLeftSubtree);
        firstChild.RemoveKey(biggestKeyFromLeftSubtree);
        if (!firstChild.HasTooFewKeys())
        {
            return;
        }

        MergeNodes(firstChild, node);
    }

    private int GetIndexOfItemBetweenSeparators(T item, Node<T> node)
    {
        for (int i = 1; i < node.KeyCount; i++)
        {
            if (item.CompareTo(node.Keys[i - 1]) > 0 && item.CompareTo(node.Keys[i]) < 0)
            {
                return i;
            }
        }

        return -1;
    }
}
