using Collections;
using Xunit;

namespace CircularDoublyLinkedListFacts;

public class HashTableDictionaryFacts
{
    [Fact]
    public void AddSeveralElementsInDictionary()
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
    public void ClearDictionaryAfterAddingSeveralElements()
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
        
        dictionary.Clear();
        Assert.Equal(0, dictionary.Count);
        dictionary.Add(2, "a");
        dictionary.Add(3, "b");
        Assert.Equal(2, dictionary.Count);

        Assert.Equal(new HashTableDictionary<int, string>(5) {{2, "a"}, {3, "b"}},
            dictionary);

    }
}