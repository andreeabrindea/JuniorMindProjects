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
    public void RemoveAKeyFromANodeWithSufficientNoOfKeys()
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

        Assert.True(btree.Remove(97));
        Assert.False(btree.Contains(97));
    }

    [Fact]
    public void RemoveAKeyFromANodeWithInsufficientNoOfKeys()
    {
        BTreeCollection<int> btree = new();
        btree.Add(9);
        btree.Add(15);
        btree.Add(20);
        btree.Add(6);
        btree.Add(8);
        btree.Add(10);
        btree.Add(12);
        btree.Add(14);
        btree.Add(16);
        btree.Add(18);
        btree.Add(21);
        btree.Add(22);
        btree.Add(24);
        
        Assert.True(btree.Remove(16));
        Assert.False(btree.Contains(16));
    }
       [Fact]
    public void AddDuplicateKeys()
    {
        BTreeCollection<int> btree = new();
        btree.Add(10);
        btree.Add(10);
        Assert.Equal(1, btree.Count); // Ensure no duplicate is added
        Assert.True(btree.Contains(10));
    }

    [Fact]
    public void RemoveNonExistentKey()
    {
        BTreeCollection<int> btree = new();
        btree.Add(5);
        btree.Add(15);
        Assert.False(btree.Remove(10));
    }

    [Fact]
    public void RemoveRootKey()
    {
        BTreeCollection<int> btree = new();
        btree.Add(5);
        Assert.True(btree.Remove(5)); 
        Assert.Equal(0, btree.Count);
        Assert.False(btree.Contains(5));
    }

    [Fact]
    public void RemoveFromSingleLeafNode()
    {
        BTreeCollection<int> btree = new();
        btree.Add(5);
        btree.Add(3);
        btree.Add(7);

        Assert.True(btree.Remove(3));
        Assert.False(btree.Contains(3));
    }

    [Fact]
    public void SearchNonExistentKey()
    {
        BTreeCollection<int> btree = new();
        btree.Add(1);
        btree.Add(5);
        btree.Add(10);
        Assert.Null(btree.Search(2));
    }

    [Fact]
    public void AddMultipleKeysWithDegreeTwo()
    {
        BTreeCollection<int> btree = new(2);
        btree.Add(30);
        btree.Add(10);
        btree.Add(20);
        btree.Add(40);

        Assert.True(btree.Contains(30));
        Assert.True(btree.Contains(10));
        Assert.True(btree.Contains(20));
        Assert.True(btree.Contains(40));
        Assert.Equal(4, btree.Count);
    }

    [Fact]
    public void HandleMultipleNodeSplits()
    {
        BTreeCollection<int> btree = new(3);
        btree.Add(10);
        btree.Add(20);
        btree.Add(5);
        btree.Add(6);
        btree.Add(12);
        btree.Add(30);
        btree.Add(7);
        btree.Add(17);

        Assert.True(btree.Contains(17));
        Assert.True(btree.Contains(10));
        Assert.True(btree.Contains(6)); 
        Assert.Equal(8, btree.Count);
    }
    

    [Fact]
    public void RemoveRootWithSingleChild()
    {
        BTreeCollection<int> btree = new();
        btree.Add(10);
        btree.Add(5);
        Assert.True(btree.Remove(10));
        Assert.False(btree.Contains(10));
        Assert.True(btree.Contains(5));
    }

    [Fact]
    public void CopyEmptyTreeToArray()
    {
        BTreeCollection<int> btree = new();
        int[] array = new int[btree.Count];
        btree.CopyTo(array, 0);
        Assert.Empty(array);
    }

    [Fact]
    public void ClearEmptyTree()
    {
        BTreeCollection<int> btree = new();
        btree.Clear();
        Assert.Equal(0, btree.Count);
    }

    [Fact]
    public void RemoveKeyFromInternalNode()
    {
        BTreeCollection<int> btree = new();
        btree.Add(10);
        btree.Add(20);
        btree.Add(5);
        btree.Add(7);
        btree.Add(15);
        btree.Add(25);

        Assert.True(btree.Remove(20)); 
        Assert.False(btree.Contains(20));
        Assert.True(btree.Contains(15));
    }

    [Fact]
    public void RemoveAllKeysAndVerifyTreeEmpty()
    {
        BTreeCollection<int> btree = new();
        btree.Add(10);
        btree.Add(20);
        btree.Add(30);

        Assert.True(btree.Remove(10));
        Assert.True(btree.Remove(20));
        Assert.True(btree.Remove(30));

        Assert.Equal(0, btree.Count);
        Assert.False(btree.Contains(10));
        Assert.False(btree.Contains(20));
        Assert.False(btree.Contains(30));
    }

    [Fact]
    public void RemoveRoot()
    {
        BTreeCollection<int> btree = new(3);
        btree.Add(10);
        btree.Add(20);
        btree.Add(5);
        btree.Add(30);
        btree.Add(40);

        Assert.True(btree.Remove(20));
        Assert.False(btree.Contains(20));
        Assert.True(btree.Contains(5));
        Assert.True(btree.Contains(10));
        Assert.True(btree.Contains(30));
        Assert.True(btree.Contains(40));
    }
    
    
    
    [Fact]
    public void NonRecursiveAddSeveralKeys()
    {
        BTreeCollection<int> btree = new(4);
        btree.NonRecursiveAdd(59);
        btree.NonRecursiveAdd(7);
        btree.NonRecursiveAdd(23);
        btree.NonRecursiveAdd(73);
        btree.NonRecursiveAdd(97);
        btree.NonRecursiveAdd(5);
        btree.NonRecursiveAdd(2);
        btree.NonRecursiveAdd(12);
        btree.NonRecursiveAdd(67);
        
        Assert.True(btree.NonRecursiveContains(7));
        Assert.True(btree.NonRecursiveContains(59));
        Assert.True(btree.NonRecursiveContains(23));
        Assert.True(btree.NonRecursiveContains(73));
        Assert.True(btree.NonRecursiveContains(97));
        Assert.False(btree.NonRecursiveContains(66));
    }
}