namespace BTree.Facts;
using Xunit;
public class BTreeFacts
{
    [Fact]
    public void AddAKeyAndSearchForIt()
    {
        BTreeCollection<int> btree = new();
        btree.Add(2);
        Assert.True(btree.Search(2));
        Assert.False(btree.Search(3));
    }

    [Fact]
    public void AddSeveralKeys()
    {
        BTreeCollection<int> btree = new(4);
        btree.Add(59);
        btree.Add(7);
        btree.Add(23);
        btree.Add(73);
        btree.Add(97);
        Assert.True(btree.Search(7));
        Assert.True(btree.Search(59));
        Assert.True(btree.Search(23));
        Assert.True(btree.Search(73));
        Assert.True(btree.Search(97));
        Assert.False(btree.Search(66));

    }
}