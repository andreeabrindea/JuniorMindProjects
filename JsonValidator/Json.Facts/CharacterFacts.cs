using Xunit;

namespace Json.Facts
{
    public class CharacterFacts
    {
        [Fact]
        public void IsNotNull()
        {
            Character character = new Character('a');
            Assert.False(character.Match(null));
            
        }

        [Fact]
        public void IsNotEmpty()
        {
            Character character = new Character('a');
            Assert.False(character.Match(""));
        }

        [Fact]
        public void HasPattern()
        {
            Character character = new Character('a');
            Assert.True(character.Match("abcd"));
        }
        
        [Fact]
        public void DoesNotHavePattern()
        {
            Character character = new Character('x');
            Assert.False(character.Match("abcd"));
        }
    }
}