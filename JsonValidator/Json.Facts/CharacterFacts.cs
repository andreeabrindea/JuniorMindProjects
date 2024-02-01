using Xunit;

namespace Json.Facts
{
    public class CharacterFacts
    {
        [Fact]
        public void IsNotNull()
        {
            Character character = new Character('a');
            Assert.False(character.Match(null).Success());
            Assert.Null(character.Match(null).RemainingText());
        }

        [Fact]
        public void IsNotEmpty()
        {
            Character character = new Character('a');
            Assert.False(character.Match("").Success());
            Assert.Equal("", character.Match("").RemainingText());
        }

        [Fact]
        public void HasPattern()
        {
            Character character = new Character('a');
            Assert.True(character.Match("abcd").Success());
            Assert.Equal("bcd", character.Match("abcd").RemainingText());
        }
        
        [Fact]
        public void DoesNotHavePattern()
        {
            Character character = new Character('x');
            Assert.False(character.Match("abcd").Success());
            Assert.Equal("abcd", character.Match("abcd").RemainingText());
        }
    }
}