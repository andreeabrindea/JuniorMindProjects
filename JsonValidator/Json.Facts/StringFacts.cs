using Xunit;
using static Json.JsonString;

namespace Json.Facts
{
    public class StringFacts
    {
        [Fact]
        public void IsWrappedInDoubleQuotes()
        {
            var stringPattern = new String();
            Assert.True(stringPattern.Match(Quoted("abc")).Success());
        }

        [Fact]
        public void AlwaysStartsWithQuotes()
        {
            var stringPattern = new String();
            Assert.False(stringPattern.Match("abc\"").Success());
        }

        [Fact]
        public void AlwaysEndsWithQuotes()
        {
            var stringPattern = new String();
            Assert.False(stringPattern.Match("\"abc").Success());
        }

        [Fact]
        public void IsNotNull()
        {
            var stringPattern = new String();
            Assert.False(stringPattern.Match(null).Success());
        }

        [Fact]
        public void IsNotAnEmptyString()
        {
            var stringPattern = new String();
            Assert.False(stringPattern.Match(string.Empty).Success());
        }

        [Fact]
        public void IsAnEmptyDoubleQuotedString()
        {
            var stringPattern = new String();
            Assert.True(stringPattern.Match(Quoted(string.Empty)).Success());
        }

        [Fact]
        public void DoesNotContainControlCharacters()
        {
            var stringPattern = new String();
            Assert.False(stringPattern.Match(Quoted("a\nb\rc")).Success());
        }

        [Fact]
        public void CanContainLargeUnicodeCharacters()
        {
            var stringPattern = new String();
            Assert.True(stringPattern.Match(Quoted("⛅⚾")).Success());
        }

        [Fact]
        public void CanContainEscapedQuotationMark()
        {
            var stringPattern = new String();
            Assert.True(stringPattern.Match(Quoted(@"\""a\"" b")).Success());
        }

        [Fact]
        public void CanContainEscapedReverseSolidus()
        {
            var stringPattern = new String();
            Assert.True(stringPattern.Match(Quoted(@"a \\ b")).Success());
        }

        [Fact]
        public void CanContainEscapedSolidus()
        {
            var stringPattern = new String();
            Assert.True(stringPattern.Match(Quoted(@"a \/ b")).Success());
        }

        [Fact]
        public void CanContainEscapedBackspace()
        {
            var stringPattern = new String();
            Assert.True(stringPattern.Match(Quoted(@"a \b b")).Success());
        }

        [Fact]
        public void CanContainEscapedFormFeed()
        {
            var stringPattern = new String();
            Assert.True(stringPattern.Match(Quoted(@"a \f b")).Success());
        }

        [Fact]
        public void CanContainEscapedLineFeed()
        {
            var stringPattern = new String();
            Assert.True(stringPattern.Match(Quoted(@"a \n b")).Success());
        }

        [Fact]
        public void CanContainEscapedCarrigeReturn()
        {
            var stringPattern = new String();
            Assert.True(stringPattern.Match(Quoted(@"a \r b")).Success());
        }

        [Fact]
        public void CanContainEscapedHorizontalTab()
        {
            var stringPattern = new String();
            Assert.True(stringPattern.Match(Quoted(@"a \t b")).Success());
        }

        [Fact]
        public void CanContainEscapedUnicodeCharacters()
        {
            var stringPattern = new String();
            Assert.True(stringPattern.Match(Quoted(@"a \u26Be b")).Success());
        }

        [Fact]
        public void CanContainAnyMultipleEscapeSequences()
        {
            var stringPattern = new String();
            Assert.True(stringPattern.Match(Quoted(@"\\\u1212\n\t\r\\\b")).Success());
        }

        [Fact]
        public void DoesNotContainUnrecognizedExcapceCharacters()
        {
            var stringPattern = new String();
            Assert.False(stringPattern.Match(Quoted(@"a\x")).Success());
        }

        [Fact]
        public void DoesNotEndWithReverseSolidus()
        {
            var stringPattern = new String();
            Assert.False(stringPattern.Match(Quoted(@"a\")).Success());
        }

        [Fact]
        public void DoesNotEndWithAnUnfinishedHexNumber()
        {
            var stringPattern = new String();
            Assert.False(stringPattern.Match(Quoted(@"a\u")).Success());
            Assert.False(stringPattern.Match(Quoted(@"a\u123")).Success());
        }

        [Fact]
        public void DoesEndWithTwoReverseSolidus()
        {
            var stringPattern = new String();
            Assert.True(stringPattern.Match(Quoted(@"a\\")).Success());
        }
        
        [Fact]
        public void DoesNotEndWithThreeReverseSolidus()
        {
            var stringPattern = new String();
            Assert.False(stringPattern.Match(Quoted(@"a\\\")).Success());
        }

        public static string Quoted(string text)
            => $"\"{text}\"";
    }
}