using Xunit;
namespace RadixTree.Facts;

public class RadixTreeFacts
{
    [Fact]
    public void InsertWord()
    {
        RadixTree tree = new();
        tree.Add("hello");
        Assert.Equal(new RadixTree { "hello" }, tree);
    }

    [Fact]
    public void InsertMultipleWordsWithSamePrefix()
    {
        RadixTree tree = new();
        tree.Add("hello");
        tree.Add("he");
        Assert.Equal(new RadixTree { "hello", "he" }, tree);
    }
}