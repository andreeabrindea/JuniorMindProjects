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
        if (Count > 0)
        {
            return;
        }

        root.AddKey(item);
        root.IsLeaf = false;
        Count++;
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
