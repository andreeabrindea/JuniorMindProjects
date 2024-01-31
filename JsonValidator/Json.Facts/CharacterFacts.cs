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
            
        }

        [Fact]
        public void IsNotEmpty()
        {
            Character character = new Character('a');
            Assert.False(character.Match("").Success());
        }

        [Fact]
        public void HasPattern()
        {
            Character character = new Character('a');
            
            Assert.True(character.Match("abcd").Success());
        }
        
        [Fact]
        public void DoesNotHavePattern()
        {
            Character character = new Character('x');
            Assert.False(character.Match("abcd").Success());
        }
    }
}