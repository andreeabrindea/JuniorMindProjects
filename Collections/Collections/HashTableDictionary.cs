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
    }

    public ICollection<TKey> Keys { get; }

    public ICollection<TValue> Values { get; }

    public int Count { get; set; }

    public bool IsReadOnly => false;

    public TValue this[TKey key]
    {
        get => TryGetValue(key, out var value) ? value : default;

        set
        {
           int bucketIndex = GetBucketIndex(key);
           for (int i = buckets[bucketIndex]; i != -1; i = elements[i].Next)
           {
               if (elements[i].Key.Equals(key))
               {
                   elements[i].Value = value;
               }
           }
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

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return TryGetValue(item.Key, out var value) && item.Value.Equals(value);
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

        if (buckets[bucketIndex] == indexOfElementToBeRemoved)
        {
            buckets[bucketIndex] = elements[indexOfElementToBeRemoved].Next;
        }

        int previousElementIndex = GetPreviousElementIndex(item, indexOfElementToBeRemoved);
        elements[previousElementIndex].Next = elements[indexOfElementToBeRemoved].Next;

        elements[indexOfElementToBeRemoved].Key = default;
        elements[indexOfElementToBeRemoved].Value = default;

        Count--;
        return true;
    }

    public void Add(TKey key, TValue value)
    {
        if (ContainsKey(key))
        {
            return;
        }

        if (Count == elements.Length)
        {
            int resizeValue = buckets.Length * 2;
            Array.Resize(ref elements, resizeValue);
        }

        int bucketIndex = GetBucketIndex(key);
        int elementIndex = Count++;
        elements[elementIndex] = new Element<TKey, TValue>(key, value);

        elements[elementIndex].Next = buckets[bucketIndex];
        buckets[bucketIndex] = elementIndex;
    }

    public bool ContainsKey(TKey key)
    {
        return TryGetValue(key, out _);
    }

    public bool Remove(TKey key)
    {
        TryGetValue(key, out var value);
        KeyValuePair<TKey, TValue> item = new KeyValuePair<TKey, TValue>(key, value);
        return Remove(item);
    }

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

    private int GetBucketIndex(TKey key)
    {
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
}