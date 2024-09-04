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

    [Fact]
    public void CopyBTreeToArray()
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
        int[] array = new int[btree.Count];
        int arrayIndex = 0;
        btree.CopyTo(array, arrayIndex);
        Assert.Equal(7, array[0]);
        Assert.Equal(59, array[1]);
        Assert.Equal(2, array[2]);
        Assert.Equal(5, array[3]);
        Assert.Equal(12, array[4]);
        Assert.Equal(23, array[5]);
        Assert.Equal(67, array[6]);
        Assert.Equal(73, array[7]);
        Assert.Equal(97, array[8]);
    }

    [Fact]
    public void ClearBTree()
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
        btree.Clear();
        Assert.Equal(0, btree.Count);
        int[] array = new int[btree.Count];
        int arrayIndex = 0;
        btree.CopyTo(array, arrayIndex);
        Assert.Throws<IndexOutOfRangeException>(() => array[0]);
    }

    [Fact]
    public void DeleteAKeyFromANodeWithSufficientNoOfKeys()
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

        btree.Remove(97);
        Assert.False(btree.Contains(97));
    }
}