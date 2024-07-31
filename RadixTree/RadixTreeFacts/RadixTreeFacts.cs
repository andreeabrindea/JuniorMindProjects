namespace RadixTreeStructure.Facts;

using Xunit;

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

    [Fact]
    public void InsertSeveralWordsWithSamePrefix()
    {
        RadixTree tree = new();
        tree.Add("hello");
        tree.Add("he");
        tree.Add("help");
        tree.Add("hell");
        tree.Add("apple");
        tree.Add("app");
        Assert.Equal(new RadixTree { "hel", "hello", "help", "apple" }, tree);
    }


    [Fact]
    public void SearchForExistingWord()
    {
        RadixTree tree = new();
        tree.Add("hello");
        Assert.True(tree.Search("hello"));
    }

    [Fact]
    public void SearchForNonExistingWord()
    {
        RadixTree tree = new();
        tree.Add("hello");
        Assert.False(tree.Search("abc"));
    }

    [Fact]
    public void SearchForExistingPrefix()
    {
        RadixTree tree = new();
        tree.Add("hello");
        tree.Add("here");
        Assert.True(tree.Search("he"));
    }

    [Fact]
    public void DeleteExistingNode()
    {
        RadixTree tree = new();
        tree.Add("hello");
        tree.Delete("hello");
        Assert.False(tree.Search("hello"));
    }

    [Fact]
    public void DeleteNonExistingNode()
    {
        RadixTree tree = new();
        tree.Add("hello");
        tree.Delete("he");
        Assert.False(tree.Search("he"));
    }
}