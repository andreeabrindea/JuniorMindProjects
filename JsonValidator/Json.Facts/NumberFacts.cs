using Xunit;
namespace Json.Facts;

public class NumberFacts
{
        [Fact]
        public void CanBeZero()
        {
            var number = new Number();
            Assert.True(number.Match("0").Success());
            Assert.Equal("", number.Match("0").RemainingText());
        }

        [Fact]
        public void DoesNotContainLetters()
        {
            var number = new Number();
            Assert.False(number.Match("a").Success());
            Assert.Equal("a", number.Match("a").RemainingText());
        }

        [Fact]
        public void CanHaveASingleDigit()
        {
            var number = new Number();
            Assert.True(number.Match("7").Success());
            Assert.Equal("", number.Match("7").RemainingText());
        }

        [Fact]
        public void CanHaveMultipleDigits()
        {
            var number = new Number();
            Assert.True(number.Match("70").Success());
            Assert.Equal("", number.Match("70").RemainingText());
        }

        [Fact]
        public void IsNotNull()
        {
            var number = new Number();
            Assert.False(number.Match(null).Success());
            Assert.Null(number.Match(null).RemainingText());
        }

        [Fact]
        public void IsNotAnEmptyString()
        {   
            var number = new Number();
            Assert.False(number.Match(string.Empty).Success());
            Assert.Equal(string.Empty, number.Match(string.Empty).RemainingText());
        }

        [Fact]
        public void DoesNotStartWithZero()
        {
            var number = new Number();
            Assert.False(number.Match("07").Success());
            Assert.Equal("7", number.Match("07").RemainingText());
        }

        [Fact]
        public void CanBeNegative()
        {
            var number = new Number();
            Assert.True(number.Match("-26").Success());
            Assert.Equal("", number.Match("-26").RemainingText());
        }

        [Fact]
        public void CanBeMinusZero()
        {
            var number = new Number();
            Assert.True(number.Match("-0").Success());
            Assert.Equal("", number.Match("-0").RemainingText());
        }

        [Fact]
        public void CanBeFractional()
        {
            var number = new Number();
            Assert.True(number.Match("12.34").Success());
            Assert.Equal("", number.Match("12.34").RemainingText());
        }

        [Fact]
        public void TheFractionCanHaveLeadingZeros()
        {
            var number = new Number();
            Assert.True(number.Match("0.00000001").Success());
            Assert.Equal("", number.Match("0.00000001").RemainingText());
            
            Assert.True(number.Match("10.00000001").Success());
            Assert.Equal("", number.Match("10.00000001").RemainingText());
        }
        
        [Fact]
        public void DoesNotEndWithADot()
        {
            var number = new Number();
            Assert.False(number.Match("12.").Success());
            Assert.Equal(".", number.Match("12.").RemainingText());
        }

        [Fact]
        public void DoesNotHaveTwoFractionParts()
        {
            var number = new Number();
            Assert.False(number.Match("12.34.56").Success());
            Assert.Equal(".56", number.Match("12.34.56").RemainingText());
        }

        [Fact]
        public void TheDecimalPartDoesNotAllowLetters()
        {
            var number = new Number();
            Assert.False(number.Match("12.3x").Success());
        }

        [Fact]
        public void CanHaveAnExponent()
        {
            var number = new Number();
            Assert.True(number.Match("12e3").Success());
        }

        [Fact]
        public void TheExponentCanStartWithCapitalE()
        {
            var number = new Number();
            Assert.True(number.Match("12E3").Success());
        }

        [Fact]
        public void TheExponentCanHavePositive()
        {
            var number = new Number();
            Assert.True(number.Match("12e+3").Success());
        }

        [Fact]
        public void TheExponentCanBeNegative()
        {
            var number = new Number();
            Assert.True(number.Match("61e-9").Success());
        }

        [Fact]
        public void CanHaveFractionAndExponent()
        {
            var number = new Number();
            Assert.True(number.Match("12.34E3").Success());
        }

        [Fact]
        public void TheExponentDoesNotAllowLetters()
        {
            var number = new Number();
            Assert.False(number.Match("22e3x3").Success());
        }

        [Fact]
        public void DoesNotHaveTwoExponents()
        {
            var number = new Number();
            Assert.False(number.Match("22e323e33").Success());
        }

        [Fact]
        public void TheExponentIsAlwaysComplete()
        {
            var number = new Number();
            Assert.False(number.Match("22e").Success());
            Assert.False(number.Match("22e+").Success());
            Assert.False(number.Match("23E-").Success());
        }

        [Fact]
        public void TheExponentIsAfterTheFraction()
        {
            var number = new Number();
            Assert.False(number.Match("22e3.3").Success());
        }
}