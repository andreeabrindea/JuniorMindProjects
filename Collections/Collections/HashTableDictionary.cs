using System.Collections;

namespace Collections;

public class HashTableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
    private readonly int[] buckets;
    private Element<TKey, TValue>[] elements;

    public HashTableDictionary(int capacity)
    {
        Count = 0;
        buckets = new int[capacity];
        for (int i = 0; i < capacity; i++)
        {
            buckets[i] = -1;
        }

        int initialNumberOfElements = capacity * 2;
        elements = new Element<TKey, TValue>[initialNumberOfElements];
        FreeIndex = -1;
    }

    public ICollection<TKey> Keys { get; }

    public ICollection<TValue> Values { get; }

    public int Count { get; set; }

    public int FreeIndex { get; private set; }

    public bool IsReadOnly => false;

    public TValue this[TKey key]
    {
        get
        {
            TryGetValue(key, out var value);
            if (!value.Equals(default))
            {
                return value;
            }

            throw new KeyNotFoundException();
        }

        set
        {
            int bucketIndex = GetBucketIndex(key);
            bool found = false;
            for (int i = buckets[bucketIndex]; i != -1; i = elements[i].Next)
            {
                if (elements[i].Key.Equals(key))
                {
                    elements[i].Value = value;
                    found = true;
                }
            }

            if (found)
            {
                return;
            }

            throw new KeyNotFoundException();
        }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        for (int i = 0; i < buckets.Length; i++)
        {
            for (int j = buckets[i]; j != -1; j = elements[j].Next)
            {
                yield return elements[j].KeyValue();
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(TKey key, TValue value)
    {
        if (ContainsKey(key))
        {
            return;
        }

        CheckResizeArray();

        int bucketIndex = GetBucketIndex(key);
        int nextFreeIndex = GetNextFreeIndex();

        elements[nextFreeIndex] = new Element<TKey, TValue>(key, value);

        elements[nextFreeIndex].Next = buckets[bucketIndex];
        buckets[bucketIndex] = nextFreeIndex;
    }

    public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

    public void Clear()
    {
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = -1;
        }

        int initialSize = buckets.Length * 2;
        elements = new Element<TKey, TValue>[initialSize];
        Count = 0;
    }

    public bool Contains(KeyValuePair<TKey, TValue> item) => TryGetValue(item.Key, out var value) && item.Value.Equals(value);

    public bool ContainsKey(TKey key) => TryGetValue(key, out _);

    public bool TryGetValue(TKey key, out TValue value)
    {
        ArgumentNullException.ThrowIfNull(key);
        int bucketIndex = GetBucketIndex(key);
        for (int i = buckets[bucketIndex]; i != -1; i = elements[i].Next)
        {
            if (elements[i].Key.Equals(key))
            {
                value = elements[i].Value;
                return true;
            }
        }

        value = default;
        return false;
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        if (!Contains(item))
        {
            return false;
        }

        int bucketIndex = GetBucketIndex(item.Key);
        int indexOfElementToBeRemoved = GetIndexOfElement(item);

        if (indexOfElementToBeRemoved == -1)
        {
            return false;
        }

        if (buckets[bucketIndex] == indexOfElementToBeRemoved)
        {
            buckets[bucketIndex] = elements[indexOfElementToBeRemoved].Next;
        }
        else
        {
            int previousElementIndex = GetPreviousElementIndex(item, indexOfElementToBeRemoved);
            if (previousElementIndex != -1)
            {
                elements[previousElementIndex].Next = elements[indexOfElementToBeRemoved].Next;
            }
        }

        elements[indexOfElementToBeRemoved].Key = default;
        elements[indexOfElementToBeRemoved].Value = default;
        elements[indexOfElementToBeRemoved].Next = FreeIndex;
        FreeIndex = indexOfElementToBeRemoved;

        Count--;
        return true;
    }

    public bool Remove(TKey key)
    {
        TryGetValue(key, out var value);
        return Remove(new KeyValuePair<TKey, TValue>(key, value));
    }

    private int GetBucketIndex(TKey key)
    {
        if (key.GetHashCode() == 0)
        {
            return 0;
        }

        return buckets.Length >= Math.Abs(key.GetHashCode())
            ? buckets.Length % Math.Abs(key.GetHashCode())
            : Math.Abs(key.GetHashCode()) % buckets.Length;
    }

    private int GetIndexOfElement(KeyValuePair<TKey, TValue> item)
    {
        int bucketIndex = GetBucketIndex(item.Key);
        for (int i = buckets[bucketIndex]; i != -1; i = elements[i].Next)
        {
            if (elements[i].KeyValue().Equals(item))
            {
                return i;
            }
        }

        return -1;
    }

    private int GetPreviousElementIndex(KeyValuePair<TKey, TValue> item, int itemIndex)
    {
        int bucketIndex = GetBucketIndex(item.Key);
        for (int i = buckets[bucketIndex]; i != -1; i = elements[i].Next)
        {
            if (elements[i].Next.Equals(itemIndex))
            {
                return i;
            }
        }

        return -1;
    }

    private int GetNextFreeIndex()
    {
        if (FreeIndex == -1)
        {
            return Count++;
        }

        var nextFreeIndex = FreeIndex;
        FreeIndex = elements[FreeIndex].Next;
        return nextFreeIndex;
    }

    private void CheckResizeArray()
    {
        if (Count < elements.Length)
        {
            return;
        }

        int resizeValue = buckets.Length * 2;
        Array.Resize(ref elements, resizeValue);
    }
}