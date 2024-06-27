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
}