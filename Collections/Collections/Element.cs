namespace Collections;

public class Element<TKey, TValue>
{
    private readonly int next;

    public Element(TKey key, TValue value)
    {
        Key = key;
        Value = value;
        next = -1;
    }

    public int Next { get; set; }

    public TValue Value { get; set; }

    public TKey Key { get; set; }

    public KeyValuePair<TKey, TValue> KeyValue()
    {
        return new(Key, Value);
    }
}