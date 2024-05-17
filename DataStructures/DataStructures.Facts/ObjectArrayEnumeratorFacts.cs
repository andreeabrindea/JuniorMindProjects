using Xunit;

namespace DataStructures.Facts;

public class ObjectArrayEnumeratorFacts
{
    [Fact]
    public void MoveNextOnePositionWhenArrayHas3Elements()
    {
        ObjectArray objectArray = new() {"ello", 1, "abc"};
        ObjectArrayEnumerator enumerator = objectArray.GetEnumerator();
        enumerator.MoveNext();
        Assert.Equal("ello", enumerator.Current);
    }

    [Fact]
    public void MoveNext2PositionAtTheEndOfArray()
    {
        ObjectArray objectArray = new() {"ello", 1};
        ObjectArrayEnumerator enumerator = objectArray.GetEnumerator();
        enumerator.MoveNext();
        enumerator.MoveNext();
        Assert.Equal(1, enumerator.Current);
        enumerator.MoveNext();
        Assert.Null(enumerator.Current);
    }
    
}