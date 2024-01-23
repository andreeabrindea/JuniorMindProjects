using Xunit;

namespace Json.Facts
{

    public class RangeFacts
    {
        [Fact]
        public void IsEmpty()
        {
            Range range = new Range('a', 'z');
            Assert.False(range.Match(""));
        }

        [Fact]
        public void IsNull()
        {
            Range range = new Range('a', 'z');
            Assert.False(range.Match(null));
        }
        
        [Fact]
        public void LengthIsOne()
        {
            Range range = new Range('a', 'z');
            Assert.True(range.Match("a"));
        }

        [Fact]
        public void StartCharacterIsBiggerThanEndCharacter()
        {
            Range range = new Range('z', 'a');
            Assert.False(range.Match("a"));
        }

        [Fact]
        public void IsInRange()
        {
            Range range = new Range('a', 'f');
            Assert.True(range.Match("fab"));
        }

        [Fact]
        public void IsNotInRange()
        {
            Range range = new Range('a', 'f');
            Assert.False(range.Match("1abc"));
        }
    }
}