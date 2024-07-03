using Collections;
using Xunit;

namespace CircularDoublyLinkedListFacts;

public class HashTableDictionaryFacts
{
    [Fact]
    public void AddSeveralElementsWithIntKeyInDictionary()
    {
        HashTableDictionary<int, string> dictionary = new(5);
        dictionary.Add(2, "a");
        dictionary.Add(3, "b");
        dictionary.Add(4, "c");
        dictionary.Add(5, "d");
        dictionary.Add(6, "e");
        dictionary.Add(7, "f");
        dictionary.Add(8, "g");
        dictionary.Add(9, "h");
        Assert.Equal(new HashTableDictionary<int, string>(5) {{2, "a"}, {3, "b"}, {4, "c"}, {5, "d"}, {6, "e"}, {7, "f"}, {8, "g"}, {9, "h"}},
            dictionary);
    }
    
    [Fact]
    public void AddSeveralElementsWithStringKeyInDictionary()
    {
        HashTableDictionary<string, string> dictionary = new(5);
        dictionary.Add("a", "a");
        dictionary.Add("b", "b");
        dictionary.Add("c", "c");
        dictionary.Add("d", "d");
        dictionary.Add("e", "e");
        dictionary.Add("f", "f");
        dictionary.Add("g", "g");
        dictionary.Add("h", "h");
        Assert.Equal(new HashTableDictionary<string, string>(5) {{"a", "a"}, {"b", "b"}, {"c", "c"}, {"d", "d"}, {"e", "e"}, {"f", "f"}, {"g", "g"}, {"h", "h"}},
            dictionary);
    }

    [Fact]
    public void ClearDictionaryAfterAddingSeveralElements()
    {
        HashTableDictionary<int, string> dictionary = new(5)
        {
            { 2, "a" },
            { 3, "b" },
            { 4, "c" },
            { 5, "d" },
            { 6, "e" },
            { 7, "f" },
            { 8, "g" },
            { 9, "h" }
        };
        
        dictionary.Clear();
        Assert.Equal(0, dictionary.Count);
        dictionary.Add(2, "a");
        dictionary.Add(3, "b");
        Assert.Equal(2, dictionary.Count);

        Assert.Equal(new HashTableDictionary<int, string>(5) {{2, "a"}, {3, "b"}},
            dictionary);
    }

    [Fact]
    public void TryGetValueByIntegerKey()
    {
        HashTableDictionary<int, string> dictionary = new(5)
        {
            { 2, "a" },
            { 3, "b" },
            { 4, "c" },
            { 5, "d" },
            { 6, "e" },
            { 7, "f" },
            { 8, "g" },
            { 9, "h" }
        };
        
        Assert.True(dictionary.TryGetValue(6, out _));
        Assert.False(dictionary.TryGetValue(11, out _));
    }
    
    [Fact]
    public void TryGetValueByStringKey()
    {
        HashTableDictionary<string, int> dictionary = new(5)
        {
            {"a", 2 },
            {"b", 3 },
            {"c", 4 },
            {"d", 5 },
            {"e", 6 },
            {"f", 7 },
            {"g", 8 },
            {"h", 9 }
        };
        
        Assert.True(dictionary.TryGetValue("a", out _));
        Assert.False(dictionary.TryGetValue("z", out _));
    }

    [Fact]
    public void GetElementsByKey()
    {
        HashTableDictionary<int, string> dictionary = new(5);
        dictionary.Add(2, "a");
        dictionary.Add(5, "d");
        dictionary.Add(9, "h");
        
        Assert.Equal("a", dictionary[2]);
        Assert.Equal("h", dictionary[9]);
        Assert.Equal("d", dictionary[5]);
    }

    [Fact]
    public void SetElementByKey()
    {
        HashTableDictionary<int, string> dictionary = new(5);
        dictionary.Add(2, "a");
        dictionary.Add(3, "b");
        dictionary.Add(4, "c");
        
        Assert.Equal("a", dictionary[2]);
        dictionary[2] = "z";
        Assert.Equal("z", dictionary[2]);
        Assert.Equal("b", dictionary[3]);
    }

    [Fact]
    public void ContainsExistingElement()
    {
        HashTableDictionary<int, string> dictionary = new(5);
        dictionary.Add(2, "a");
        dictionary.Add(3, "b");
        dictionary.Add(4, "c");
        KeyValuePair<int, string> item = new KeyValuePair<int, string>(2, "a");
        Assert.True(dictionary.Contains(item));
    }
    
    [Fact]
    public void ContainsNonExistingElement()
    {
        HashTableDictionary<int, string> dictionary = new(5);
        dictionary.Add(2, "a");
        dictionary.Add(3, "b");
        dictionary.Add(4, "c");
        KeyValuePair<int, string> item = new KeyValuePair<int, string>(2, "b");
        Assert.False(dictionary.Contains(item));
    }
    
    [Fact]
    public void ContainsExistingKey()
    {
        HashTableDictionary<int, string> dictionary = new(5);
        dictionary.Add(2, "a");
        dictionary.Add(3, "b");
        dictionary.Add(4, "c");
        Assert.True(dictionary.ContainsKey(2));
    }
    
    [Fact]
    public void ContainsNonExistingKey()
    {
        HashTableDictionary<int, string> dictionary = new(5);
        dictionary.Add(2, "a");
        dictionary.Add(3, "b");
        dictionary.Add(4, "c");
        Assert.False(dictionary.ContainsKey(9));
    }

    [Fact]
    public void AddElementWithAlreadyExistingKey()
    {
        HashTableDictionary<int, string> dictionary = new(5);
        dictionary.Add(2, "a");
        dictionary.Add(3, "b");
        dictionary.Add(4, "c");
        dictionary.Add(4, "d");
        
        Assert.Equal(new HashTableDictionary<int, string>(5) {{2, "a"}, {3, "b"}, {4, "c"}},
            dictionary);
    }

    [Fact]
    public void RemoveElement()
    {
        HashTableDictionary<int, string> dictionary = new(5);
        dictionary.Add(2, "a");
        dictionary.Add(3, "b");
        dictionary.Add(4, "c");
        dictionary.Add(5, "d");
        dictionary.Add(6, "e");
        
        KeyValuePair<int, string> item = new KeyValuePair<int, string>(4, "c");
        dictionary.Remove(item);
        
        Assert.Equal(new HashTableDictionary<int, string>(5) {{2, "a"}, {3, "b"}, {5, "d"}, {6, "e"}},
            dictionary);
    }

    [Fact]
    public void RemoveElementByKey()
    {
        HashTableDictionary<int, string> dictionary = new(5);
        dictionary.Add(2, "a");
        dictionary.Add(3, "b");
        dictionary.Add(4, "c");
        dictionary.Add(5, "d");
        dictionary.Add(6, "e");
        
        KeyValuePair<int, string> item = new KeyValuePair<int, string>(4, "c");
        Assert.True(dictionary.Remove(4));
        
        Assert.Equal(new HashTableDictionary<int, string>(5) {{2, "a"}, {3, "b"}, {5, "d"}, {6, "e"}},
            dictionary);
    }
    
    [Fact]
    public void RemoveElementByNonExistingKey()
    {
        HashTableDictionary<int, string> dictionary = new(5);
        dictionary.Add(2, "a");
        dictionary.Add(3, "b");
        dictionary.Add(4, "c");
        dictionary.Add(5, "d");
        dictionary.Add(6, "e");
        
        Assert.False(dictionary.Remove(9));
        Assert.Equal(new HashTableDictionary<int, string>(5) {{2, "a"}, {3, "b"}, {4, "c"}, {5, "d"}, {6, "e"}},
            dictionary);
    }

    [Fact]
    public void ReuseIndexes()
    {
        HashTableDictionary<int, string> dictionary = new(5)
        {
            { 2, "a" },
            { 3, "b" },
            { 4, "c" },
            { 5, "d" },
            { 6, "e" }
        };

        Assert.True(dictionary.Remove(4));
        Assert.True(dictionary.Remove(5));
        Assert.Equal(new HashTableDictionary<int, string>(5) {{ 2, "a" }, { 3, "b" }, { 6, "e" }},
            dictionary);
    
        dictionary.Add(8, "f");
        dictionary.Add(10, "g");
        Assert.Equal(new HashTableDictionary<int, string>(5) {{2, "a"}, {3, "b"}, {8, "f"}, {10, "g"}, {6, "e"}},
             dictionary);
    }

    [Fact]
    public void TryAddElementWithNullKey()
    {
        HashTableDictionary<int, string> dictionary = new(5)
        {
            { 2, "a" },
            { 3, "b" },
            { 4, "c" },
            { 5, "d" },
            { 6, "e" }
        };
        KeyValuePair<int, string> item = new KeyValuePair<int, string>(default, "g");
        dictionary.Add(item);
    }

    [Fact]
    public void CopyToArray()
    {
        HashTableDictionary<int, string> dictionary = new(5)
        {
            { 2, "a" },
            { 3, "b" },
            { 4, "c" },
            { 5, "d" },
            { 6, "e" }
        };
        
        KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[5];
        dictionary.CopyTo(array, 0);
        Assert.Equal(new KeyValuePair<int, string>(5, "d"), array[0]);
        Assert.Equal(new KeyValuePair<int, string>(6, "e"), array[1]);
        Assert.Equal(new KeyValuePair<int, string>(2, "a"), array[2]);
        Assert.Equal(new KeyValuePair<int, string>(3, "b"), array[3]);
        Assert.Equal(new KeyValuePair<int, string>(4, "c"), array[4]);
    }

    [Fact]
    public void GetValues()
    {
        HashTableDictionary<int, string> dictionary = new(5)
        {
            { 2, "a" },
            { 3, "b" },
            { 4, "c" },
            { 5, "d" },
            { 6, "e" }
        };

        var values = dictionary.Values;
        Assert.Equal(new List<string>(){"a", "b", "c", "d","e"}, values);
    }
    
    [Fact]
    public void GetKeys()
    {
        HashTableDictionary<int, string> dictionary = new(5)
        {
            { 2, "a" },
            { 3, "b" },
            { 4, "c" },
            { 5, "d" },
            { 6, "e" }
        };

        var keys = dictionary.Keys;
        Assert.Equal(new List<int>(){2, 3, 4, 5, 6}, keys);
    }
}