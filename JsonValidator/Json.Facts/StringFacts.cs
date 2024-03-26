using Xunit;

namespace Json.Facts
{
    public class StringFacts
    {
        [Fact]
        public void IsWrappedInDoubleQuotes()
        {
            var stringPattern = new String();
            StringView input = new(Quoted("abc"));
            Assert.True(stringPattern.Match(input).Success());
        }

        [Fact]
        public void AlwaysStartsWithQuotes()
        {
            var stringPattern = new String();
            StringView input = new("abc\"");
            Assert.False(stringPattern.Match(input).Success());
        }

        [Fact]
        public void AlwaysEndsWithQuotes()
        {
            var stringPattern = new String();
            StringView input = new("\"abc");
            var match = stringPattern.Match(input);
            Assert.False(match.Success());
        }

        [Fact]
        public void IsNotNull()
        {
            var stringPattern = new String();
            StringView input = new(null);
            Assert.False(stringPattern.Match(input).Success());
        }

        [Fact]
        public void IsNotAnEmptyString()
        {
            var stringPattern = new String();
            StringView input = new(string.Empty);
            Assert.False(stringPattern.Match(input).Success());
        }

        [Fact]
        public void IsAnEmptyDoubleQuotedString()
        {
            var stringPattern = new String();
            StringView input = new(Quoted(string.Empty));
            Assert.True(stringPattern.Match(input).Success());
        }

        [Fact]
        public void DoesNotContainControlCharacters()
        {
            var stringPattern = new String();
            StringView input = new(Quoted("a\nb\rc"));
            Assert.False(stringPattern.Match(input).Success());
        }

        [Fact]
        public void CanContainLargeUnicodeCharacters()
        {
            var stringPattern = new String();
            StringView input = new(Quoted("⛅⚾"));
            Assert.True(stringPattern.Match(input).Success());
        }

        [Fact]
        public void CanContainEscapedQuotationMark()
        {
            var stringPattern = new String();
            StringView input = new(Quoted(@"\""a\"" b"));
            Assert.True(stringPattern.Match(input).Success());
        }

        [Fact]
        public void CanContainEscapedReverseSolidus()
        {
            var stringPattern = new String();
            StringView input = new(Quoted(@"a \\ b"));
            Assert.True(stringPattern.Match(input).Success());
        }

        [Fact]
        public void CanContainEscapedSolidus()
        {
            var stringPattern = new String();
            StringView input = new(Quoted(@"a \/ b"));
            Assert.True(stringPattern.Match(input).Success());
        }

        [Fact]
        public void CanContainEscapedBackspace()
        {
            var stringPattern = new String();
            StringView input = new(Quoted(@"a \b b"));
            Assert.True(stringPattern.Match(input).Success());
        }

        [Fact]
        public void CanContainEscapedFormFeed()
        {
            var stringPattern = new String();
            StringView input = new(Quoted(@"a \f b"));
            Assert.True(stringPattern.Match(input).Success());
        }

        [Fact]
        public void CanContainEscapedLineFeed()
        {
            var stringPattern = new String();
            StringView input = new(Quoted(@"a \n b"));
            Assert.True(stringPattern.Match(input).Success());
        }

        [Fact]
        public void CanContainEscapedCarriageReturn()
        {
            var stringPattern = new String();
            StringView input = new(Quoted(@"a \r b"));
            Assert.True(stringPattern.Match(input).Success());
        }

        [Fact]
        public void CanContainEscapedHorizontalTab()
        {
            var stringPattern = new String();
            StringView input = new(Quoted(@"a \t b"));
            Assert.True(stringPattern.Match(input).Success());
        }

        [Fact]
        public void CanContainEscapedUnicodeCharacters()
        {
            var stringPattern = new String();
            StringView input = new(Quoted(@"a \u26Be b"));
            Assert.True(stringPattern.Match(input).Success());
        }

        [Fact]
        public void CanContainAnyMultipleEscapeSequences()
        {
            var stringPattern = new String();
            StringView input = new(Quoted(@"\\\u1212\n\t\r\\\b"));
            Assert.True(stringPattern.Match(input).Success());
        }

        [Fact]
        public void DoesNotContainUnrecognizedEscapeCharacters()
        {
            var stringPattern = new String();
            StringView input = new(Quoted(@"a\x"));
            Assert.False(stringPattern.Match(input).Success());
        }

        [Fact]
        public void DoesNotEndWithReverseSolidus()
        {
            var stringPattern = new String();
            StringView input = new(Quoted(@"a\"));
            Assert.False(stringPattern.Match(input).Success());
        }

        [Fact]
        public void DoesNotEndWithAnUnfinishedHexNumber()
        {
            var stringPattern = new String();
            StringView input = new(Quoted(@"a\u"));
            Assert.False(stringPattern.Match(input).Success());
            Assert.Equal('\"', stringPattern.Match(input).RemainingText().Peek());
            
            StringView secondInput = new(Quoted(@"a\u123"));
            Assert.False(stringPattern.Match(secondInput).Success());
            Assert.Equal('\"', stringPattern.Match(secondInput).RemainingText().Peek());
        }

        [Fact]
        public void DoesEndWithTwoReverseSolidus()
        {
            var stringPattern = new String();
            StringView input = new(Quoted(@"a\\"));
            Assert.True(stringPattern.Match(input).Success());
            Assert.True(stringPattern.Match(input).RemainingText().IsEmpty());
        }
        
        [Fact]
        public void DoesNotEndWithThreeReverseSolidus()
        {
            String stringPattern = new();
            StringView input = new(Quoted(@"a\\\"));
            var match = stringPattern.Match(input);
            Assert.False(match.Success());
            Assert.Equal('\"', match.RemainingText().Peek());
        }

        private static string Quoted(string text)
            => $"\"{text}\"";
    }
}