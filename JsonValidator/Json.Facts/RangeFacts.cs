using Xunit;

namespace Json.Facts
{

    public class RangeFacts
    {
        [Fact]
        public void IsEmpty()
        {
            Range range = new('a', 'z');
            StringView input = new("");
            Assert.False(range.Match(input).Success());
            Assert.True(range.Match(input).RemainingText().IsEmpty());
        }

        [Fact]
        public void IsNull()
        {
            Range range = new('a', 'z');
            StringView input = new(null);
            Assert.False(range.Match(input).Success());
            Assert.True(range.Match(input).RemainingText().IsEmpty());
        }
        
        [Fact]
        public void LengthIsOne()
        {
            Range range = new('a', 'z');
            StringView input = new("a");
            Assert.True(range.Match(input).Success());
            Assert.True(range.Match(input).RemainingText().IsEmpty());
        }

        [Fact]
        public void StartCharacterIsBiggerThanEndCharacter()
        {
            Range range = new('z', 'a');
            StringView input = new("a");
            Assert.False(range.Match(input).Success());
            Assert.Equal('a', range.Match(input).RemainingText().Peek());
        }

        [Fact]
        public void IsInRange()
        {
            Range range = new('a', 'f');
            StringView input = new("fab");
            var match = range.Match(input);
            Assert.True(match.Success());
            Assert.Equal('a', match.RemainingText().Peek());
        }

        [Fact]
        public void IsNotInRange()
        {
            Range range = new('a', 'f');
            StringView input = new("1abc");
            Assert.False(range.Match(input).Success());
            Assert.Equal('1', range.Match(input).RemainingText().Peek());
        }
    }
}