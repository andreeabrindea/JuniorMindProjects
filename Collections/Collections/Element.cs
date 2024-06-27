namespace Collections;

public class Element<TKey, TValue>
{
    private readonly int next;

    internal Element(TKey key, TValue value)
    {
        Key = key;
        Value = value;
        next = -1;
    }

    internal int Next { get; set; }

    internal TValue Value { get; set; }

    internal TKey Key { get; set; }

    internal KeyValuePair<TKey, TValue> KeyValue()
    {
        return new(Key, Value);
    }
}