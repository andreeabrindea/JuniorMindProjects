using System.Collections;

namespace Collections;

public class HashTableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
    private Element<TKey, TValue>[] elements;
    private int[] buckets;

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
    }

    public ICollection<TKey> Keys { get; }

    public ICollection<TValue> Values { get; }

    public int Count { get; set; }

    public bool IsReadOnly => false;

    public TValue this[TKey key]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
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

    public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

    public void Clear()
    {
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = -1;
        }

        var initialSize = buckets.Length * 2;
        elements = new Element<TKey, TValue>[initialSize];
        Count = 0;
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        throw new NotImplementedException();
    }

    public void Add(TKey key, TValue value)
    {
        if (Count == buckets.Length)
        {
            int resizeValue = buckets.Length * 2;
            Array.Resize(ref buckets, resizeValue);
        }

        int bucketIndex = GetBucketIndex(key);
        int elementIndex = Count++;
        elements[elementIndex] = new Element<TKey, TValue>(key, value);

        if (buckets[bucketIndex] != -1)
        {
            int previousElementInTheSameBucket = buckets[bucketIndex];
            elements[previousElementInTheSameBucket].Next = elementIndex;
        }

        elements[elementIndex].Next = -1;
        buckets[bucketIndex] = elementIndex;
    }

    public bool ContainsKey(TKey key)
    {
        throw new NotImplementedException();
    }

    public bool Remove(TKey key)
    {
        throw new NotImplementedException();
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        throw new NotImplementedException();
    }

    public int GetBucketIndex(TKey key)
    {
        return buckets.Length >= Math.Abs(key.GetHashCode())
            ? buckets.Length % Math.Abs(key.GetHashCode())
            : Math.Abs(key.GetHashCode()) % buckets.Length;
    }
}