namespace BTree.Facts;
using Xunit;
public class BTreeFacts
{
    [Fact]
    public void Test1()
    {
        BTreeCollection<int> treeCollection = new();
        treeCollection.Add(2);
        
    }
}