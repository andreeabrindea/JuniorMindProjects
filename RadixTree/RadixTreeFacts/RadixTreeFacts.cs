using Xunit;
namespace RadixTree.Tests;

public class RadixTreeFacts
{
    [Fact]
    public void InsertWord()
    {
        RadixTree tree = new();
        tree.Add("hello");
        Assert.Equal(new RadixTree { "hello" }, tree);
    }
}