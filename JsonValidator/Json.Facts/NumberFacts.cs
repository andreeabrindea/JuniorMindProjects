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
            var match = number.Match(input);
            Assert.False(number.Match(input).Success());
            Assert.Equal('a', match.RemainingText().Peek());
        }

        [Fact]
        public void CanHaveASingleDigit()
        {
            Number number = new();
            StringView input = new("9");
            var match = number.Match(input);
            Assert.True(match.Success());
        }

        [Fact]
        public void CanHaveMultipleDigits()
        {
            Number number = new();
            StringView input = new(0,"70");
            Assert.True(number.Match(input).Success());
        }

        [Fact]
        public void IsNotNull()
        {
            Number number = new();
            StringView input = new(null);
            Assert.False(number.Match(input).Success());
        }

        [Fact]
        public void IsNotAnEmptyString()
        {   
            Number number = new();
            StringView input = new(0,string.Empty);
            Assert.False(number.Match(input).Success());
        }

        [Fact]
        public void DoesNotStartWithZero()
        {
            Number number = new();
            StringView input = new("07");
            var match = number.Match(input);
            Assert.True(match.Success());
        }

        [Fact]
        public void CanBeNegative()
        {
            Number number = new();
            StringView input = new("-26");
            Assert.True(number.Match(input).Success());
        }

        [Fact]
        public void CanBeMinusZero()
        {
            Number number = new();
            StringView input = new("-0");
            Assert.True(number.Match(input).Success());
        }

        [Fact]
        public void CanBeFractional()
        {
            Number number = new();
            List list = new(number, new Character('.'));
            StringView input = new("12.34");
            var match = list.Match(input);
            Assert.True(match.Success());
        }

        [Fact]
        public void TheFractionCanHaveLeadingZeros()
        {
            Number number = new();
            List list = new(number, new Character('.'));

            StringView input = new("0.00000001");
            var match = list.Match(input);
            Assert.True(match.Success());

            StringView input1 = new("10.00000001");
            var match1 = list.Match(input1);
            Assert.True(match1.Success());
        }
        
        [Fact]
        public void DoesNotEndWithADot()
        {
            Number number = new();
            List list = new(number, new Character('.'));

            StringView input = new("12.");
            var match = list.Match(input);
            Assert.True(match.Success());
        }

        [Fact]
        public void DoesNotHaveTwoFractionParts()
        {
            Number number = new();
            StringView input = new("12.34.56");
            var match = number.Match(input);
            Assert.True(match.Success());
        }

        [Fact]
        public void TheDecimalPartDoesNotAllowLetters()
        {
            Number number = new();
            StringView input = new("12.3x");
            var match = number.Match(input);
            Assert.True(match.Success());
        }

        [Fact]
        public void CanHaveAnExponent()
        {
            Number number = new();
            StringView input = new("12e3");
            var match = number.Match(input);
            Assert.True(match.Success());
        }

        [Fact]
        public void TheExponentCanStartWithCapitalE()
        {
            Number number = new();
            StringView input = new("12E3");
            var match = number.Match(input);
            Assert.True(match.Success());
        }

        [Fact]
        public void TheExponentCanHavePositive()
        {
            Number number = new();
            StringView input = new("12e+3");
            var match = number.Match(input);
            Assert.True(match.Success());
        }

        [Fact]
        public void TheExponentCanBeNegative()
        {
            Number number = new();
            StringView input = new("61e-9");
            var match = number.Match(input);
            Assert.True(match.Success());
        }

        [Fact]
        public void CanHaveFractionAndExponent()
        {
            Number number = new();
            List list = new(number, new Character('.'));
            StringView input = new("12.34E3");
            var match = list.Match(input);
            Assert.True(match.Success());
        }

        [Fact]
        public void TheExponentDoesNotAllowLetters()
        {
            Number number = new();
            StringView input = new("33e3x3");
            var match = number.Match(input);
            Assert.True(match.Success());
        }

        [Fact]
        public void DoesNotHaveTwoExponents()
        {
            Number number = new();
            StringView input = new("22e323e33");
            var match = number.Match(input);
            Assert.True(match.Success());
        }

        [Fact]
        public void TheExponentIsAlwaysComplete()
        {
            Number number = new();
            
            StringView input = new("22e");
            var match = number.Match(input);
            Assert.True(match.Success());

            StringView input1 = new("22e+");
            var match1 = number.Match(input1);
            Assert.True(match1.Success());

            StringView input2 = new("23E-");
            var match2 = number.Match(input2);
            Assert.True(match2.Success());
        }

        [Fact]
        public void TheExponentIsAfterTheFraction()
        {
            Number number = new();
            StringView input = new("22e3.3");
            var match = number.Match(input);
            Assert.True(match.Success());
        }
}