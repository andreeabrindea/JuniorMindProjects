namespace BTree.Facts;
using Xunit;
public class BTreeFacts
{
    [Fact]
    public void AddAKeyAndSearchForIt()
    {
        BTreeCollection<int> btree = new();
        btree.Add(2);
        
        Assert.True(btree.Contains(2));
        Node<int> node = btree.Search(2); 
        Assert.Equal(1, node.KeyCount);
        
        Assert.False(btree.Contains(3));
        Assert.Null(btree.Search(3));
        Assert.Equal(new BTreeCollection<int>{2}, btree);
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
        btree.Add(5);
        btree.Add(2);
        btree.Add(12);
        btree.Add(67);
        Assert.True(btree.Contains(7));
        Assert.True(btree.Contains(59));
        Assert.True(btree.Contains(23));
        Assert.True(btree.Contains(73));
        Assert.True(btree.Contains(97));
        Assert.False(btree.Contains(66));
    }
}