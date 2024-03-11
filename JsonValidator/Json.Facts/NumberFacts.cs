using System.Text.RegularExpressions;
using Xunit;
namespace Json.Facts;

public class NumberFacts
{
        [Fact]
        public void CanBeZero()
        {
            Number number = new();
            StringView input = new("0");
            var match = number.Match(input);
            Assert.True(match.Success());
            Assert.Equal('\0', match.RemainingText().Peek());
        }

        [Fact]
        public void DoesNotContainLetters()
        {
            Number number = new();
            StringView input = new("a");
            Assert.False(number.Match(input).Success());
            Assert.Equal('a', number.Match(input).RemainingText().Peek());
        }

        [Fact]
        public void CanHaveASingleDigit()
        {
            Number number = new();
            StringView input = new("9");
            var match = number.Match(input);
            Assert.True(match.Success());
            Assert.Equal('\0', match.RemainingText().Peek());
        }

        [Fact]
        public void CanHaveMultipleDigits()
        {
            Number number = new();
            StringView input = new("70");
            Assert.True(number.Match(input).Success());
            Assert.Equal('\0', number.Match(input).RemainingText().Peek());
        }

        [Fact]
        public void IsNotNull()
        {
            Number number = new();
            StringView input = new(null);
            Assert.False(number.Match(input).Success());
            Assert.Equal('\0', number.Match(input).RemainingText().Peek());
        }

        [Fact]
        public void IsNotAnEmptyString()
        {   
            Number number = new();
            StringView input = new(string.Empty);
            Assert.False(number.Match(input).Success());
            Assert.Equal('\0', number.Match(input).RemainingText().Peek());
        }

        [Fact]
        public void DoesNotStartWithZero()
        {
            Number number = new();
            StringView input = new("07");
            var match = number.Match(input);
            Assert.True(match.Success());
            Assert.Equal('7', match.RemainingText().Peek());
        }

        [Fact]
        public void CanBeNegative()
        {
            Number number = new();
            StringView input = new("-26");
            Assert.True(number.Match(input).Success());
            Assert.Equal('\0', number.Match(input).RemainingText().Peek());
        }

        [Fact]
        public void CanBeMinusZero()
        {
            Number number = new();
            StringView input = new("-0");
            Assert.True(number.Match(input).Success());
            Assert.Equal('\0', number.Match(input).RemainingText().Peek());
        }

        [Fact]
        public void CanBeFractional()
        {
            Number number = new();
            List list = new(number, new Character('.'));
            StringView input = new("12.34");
            var match = list.Match(input);
            Assert.True(match.Success());
            Assert.Equal('\0', match.RemainingText().Peek());
        }

        [Fact]
        public void TheFractionCanHaveLeadingZeros()
        {
            Number number = new();
            List list = new(number, new Character('.'));

            StringView input = new("0.00000001");
            var match = list.Match(input);
            Assert.True(match.Success());
            Assert.Equal('\0', match.RemainingText().Peek());

            StringView input1 = new("10.00000001");
            var match1 = list.Match(input1);
            Assert.True(match1.Success());
            Assert.Equal('\0', match1.RemainingText().Peek());
        }
        
        [Fact]
        public void DoesNotEndWithADot()
        {
            Number number = new();
            List list = new(number, new Character('.'));

            StringView input = new("12.");
            var match = list.Match(input);
            Assert.True(match.Success());
            Assert.Equal('.', match.RemainingText().Peek());
        }

        [Fact]
        public void DoesNotHaveTwoFractionParts()
        {
            Number number = new();
            StringView input = new("12.34.56");
            var match = number.Match(input);
            Assert.True(match.Success());
            Assert.Equal('.', match.RemainingText().Peek());
        }

        [Fact]
        public void TheDecimalPartDoesNotAllowLetters()
        {
            Number number = new();
            StringView input = new("12.3x");
            var match = number.Match(input);
            Assert.True(match.Success());
            Assert.Equal('x', match.RemainingText().Peek());
        }

        [Fact]
        public void CanHaveAnExponent()
        {
            Number number = new();
            StringView input = new("12e3");
            var match = number.Match(input);
            Assert.True(match.Success());
            Assert.Equal('\0', match.RemainingText().Peek());
        }

        [Fact]
        public void TheExponentCanStartWithCapitalE()
        {
            Number number = new();
            StringView input = new("12E3");
            var match = number.Match(input);
            Assert.True(match.Success());
            Assert.Equal('\0', match.RemainingText().Peek());
        }

        [Fact]
        public void TheExponentCanHavePositive()
        {
            Number number = new();
            StringView input = new("12e+3");
            var match = number.Match(input);
            Assert.True(match.Success());
            Assert.Equal('\0',  match.RemainingText().Peek());
        }

        [Fact]
        public void TheExponentCanBeNegative()
        {
            Number number = new();
            StringView input = new("61e-9");
            var match = number.Match(input);
            Assert.True(match.Success());
            Assert.Equal('\0', match.RemainingText().Peek());
        }

        [Fact]
        public void CanHaveFractionAndExponent()
        {
            Number number = new();
            List list = new(number, new Character('.'));
            StringView input = new("12.34E3");
            var match = list.Match(input);
            Assert.True(match.Success());
            Assert.Equal('\0', match.RemainingText().Peek());
        }

        [Fact]
        public void TheExponentDoesNotAllowLetters()
        {
            Number number = new();
            StringView input = new("33e3x3");
            var match = number.Match(input);
            Assert.True(match.Success());
            Assert.Equal('x', match.RemainingText().Peek());
        }

        [Fact]
        public void DoesNotHaveTwoExponents()
        {
            Number number = new();
            StringView input = new("22e323e33");
            var match = number.Match(input);
            Assert.True(match.Success());
            Assert.Equal('e', match.RemainingText().Peek());
        }

        [Fact]
        public void TheExponentIsAlwaysComplete()
        {
            Number number = new();
            
            StringView input = new("22e");
            var match = number.Match(input);
            Assert.True(match.Success());
            Assert.Equal('e', match.RemainingText().Peek());

            StringView input1 = new("22e+");
            var match1 = number.Match(input1);
            Assert.True(match1.Success());
            Assert.Equal('e', match1.RemainingText().Peek());

            StringView input2 = new("23E-");
            var match2 = number.Match(input2);
            Assert.True(match2.Success());
            Assert.Equal('E', match2.RemainingText().Peek());
        }

        [Fact]
        public void TheExponentIsAfterTheFraction()
        {
            Number number = new();
            StringView input = new("22e3.3");
            var match = number.Match(input);
            Assert.True(match.Success());
            Assert.Equal('.', match.RemainingText().Peek());
        }
}