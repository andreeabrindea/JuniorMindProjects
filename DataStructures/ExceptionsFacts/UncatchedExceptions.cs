using Xunit;
namespace Exceptions.Facts;

public class UncachedExceptions
{
    [Fact]
    public void DivisionWhenDivideByZero()
    {
        Assert.Throws<DivideByZeroException>(() => Exceptions.DivisionUncatched(17, 0));
    }
}