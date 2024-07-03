using System.Collections;

namespace Collections;

public class HashTableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
    private readonly int[] buckets;
    private Element<TKey, TValue>[] elements;
    private int freeIndex;

    public HashTableDictionary(int capacity)
    {
        Count = 0;
        buckets = new int[capacity];
        Array.Fill(buckets, -1);
        int initialNumberOfElements = capacity * 2;
        elements = new Element<TKey, TValue>[initialNumberOfElements];
        freeIndex = -1;
    }

    public ICollection<TKey> Keys
    {
        get
        {
            TKey[] keys = new TKey[Count];
            int index = 0;
            foreach (var element in elements)
            {
                if (element != null)
                {
                    keys[index] = element.Key;
                    index++;
                }
            }

            return keys;
        }
    }

    public ICollection<TValue> Values
    {
        get
        {
            TValue[] values = new TValue[Count];
            int index = 0;
            foreach (var element in elements)
            {
                if (element != null)
                {
                    values[index] = element.Value;
                    index++;
                }
            }

            return values;
        }
    }

    public int Count { get; set; }

    public bool IsReadOnly => false;

    public TValue this[TKey key]
    {
        get
        {
            ArgumentNullException.ThrowIfNull(key);
            if (!TryGetValue(key, out var value))
            {
                throw new KeyNotFoundException();
            }

            return value;
        }

        set
        {
            ArgumentNullException.ThrowIfNull(key);

            int bucketIndex = GetBucketIndex(key);
            for (int i = buckets[bucketIndex]; i != -1; i = elements[i].Next)
            {
                if (elements[i].Key.Equals(key))
                {
                    elements[i].Value = value;
                    return;
                }
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
        int nextFreeIndex = PopNextFreeIndex();

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
        ArgumentNullException.ThrowIfNull(array);

        if (arrayIndex < 0 || arrayIndex > array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        if (array.Length - arrayIndex > Count)
        {
            throw new ArgumentException("not enough space to copy", nameof(array));
        }

        foreach (var item in this)
        {
            array[arrayIndex++] = item;
        }
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        if (!Contains(item))
        {
            return false;
        }

        int bucketIndex = GetBucketIndex(item.Key);
        int indexOfElementToBeRemoved = GetIndexOfElement(item, out var previousIndex);

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
            int previousElementIndex = previousIndex;
            elements[previousElementIndex].Next = elements[indexOfElementToBeRemoved].Next;
        }

        elements[indexOfElementToBeRemoved].Key = default;
        elements[indexOfElementToBeRemoved].Value = default;
        elements[indexOfElementToBeRemoved].Next = freeIndex;
        freeIndex = indexOfElementToBeRemoved;

        Count--;
        return true;
    }

    public bool Remove(TKey key) => ContainsKey(key) && Remove(new KeyValuePair<TKey, TValue>(key, this[key]));

    private int GetIndexOfElement(KeyValuePair<TKey, TValue> item, out int previousElementIndex)
    {
        int bucketIndex = GetBucketIndex(item.Key);
        previousElementIndex = -1;
        for (int i = buckets[bucketIndex]; i != -1; i = elements[i].Next)
        {
            if (elements[i].KeyValue().Equals(item))
            {
                return i;
            }

            previousElementIndex = i;
        }

        return -1;
    }

    private int GetBucketIndex(TKey key)
    {
        if (key.GetHashCode() == 0)
        {
            return 0;
        }

        var absoluteValue = Math.Abs(key.GetHashCode());

        return buckets.Length >= absoluteValue
            ? buckets.Length % absoluteValue
            : absoluteValue % buckets.Length;
    }

    private int PopNextFreeIndex()
    {
        if (freeIndex == -1)
        {
            return Count++;
        }

        var nextFreeIndex = freeIndex;
        freeIndex = elements[freeIndex].Next;
        Count++;
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