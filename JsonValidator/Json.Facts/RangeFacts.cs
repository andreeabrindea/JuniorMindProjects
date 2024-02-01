using Xunit;

namespace Json.Facts
{

    public class RangeFacts
    {
        [Fact]
        public void IsEmpty()
        {
            Range range = new Range('a', 'z');
            Assert.False(range.Match("").Success());
            Assert.Equal("", range.Match("").RemainingText());
        }

        [Fact]
        public void IsNull()
        {
            Range range = new Range('a', 'z');
            Assert.False(range.Match(null).Success());
            Assert.Null(range.Match(null).RemainingText());
        }
        
        [Fact]
        public void LengthIsOne()
        {
            Range range = new Range('a', 'z');
            Assert.True(range.Match("a").Success());
            Assert.Equal("",range.Match("a").RemainingText());
        }

        [Fact]
        public void StartCharacterIsBiggerThanEndCharacter()
        {
            Range range = new Range('z', 'a');
            Assert.False(range.Match("a").Success());
            Assert.Equal("a", range.Match("a").RemainingText());
        }

        [Fact]
        public void IsInRange()
        {
            Range range = new Range('a', 'f');
            Assert.True(range.Match("fab").Success());
            Assert.Equal("ab", range.Match("fab").RemainingText());
        }

        [Fact]
        public void IsNotInRange()
        {
            Range range = new Range('a', 'f');
            Assert.False(range.Match("1abc").Success());
            Assert.Equal("1abc", range.Match("1abc").RemainingText());
        }
    }
}