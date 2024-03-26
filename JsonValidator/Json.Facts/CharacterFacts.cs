using Xunit;

namespace Json.Facts
{
    public class CharacterFacts
    {
        [Fact]
        public void IsNotNull()
        {
            Character character = new('a');
            StringView input = new(null);
            Assert.False(character.Match(input).Success());
            Assert.True(character.Match(input).RemainingText().IsEmpty());
        }

        [Fact]
        public void IsNotEmpty()
        {
            Character character = new('a');
            StringView input = new("");
            Assert.False(character.Match(input).Success());
            Assert.True(character.Match(input).RemainingText().IsEmpty());
        }

        [Fact]
        public void HasPattern()
        {
            Character character = new('a');
            StringView input = new("abcd");

            var match = character.Match(input);
            Assert.True(match.Success());
            Assert.Equal('b', match.RemainingText().Peek());
            Assert.Equal(1, match.Position());
        }
        
        [Fact]
        public void DoesNotHavePattern()
        {
            Character character = new('x');
            StringView input = new("abcd");
            Assert.False(character.Match(input).Success());
            Assert.Equal('a', character.Match(input).RemainingText().Peek());
        }
    }
}