using Xunit;

namespace Json.Facts;

public class SequenceFacts
{
    [Fact]
    public void IsNull()
    {
        Sequence ab = new(
            new Character('a'),
            new Character('b')
        );
        
        StringView input = new(null);
        Assert.False(ab.Match(input).Success());
        Assert.Equal('\0', ab.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void IsEmpty()
    {
        Sequence ab = new(
            new Character('a'),
            new Character('b')
        );

        StringView input = new("");
        Assert.False(ab.Match(input).Success());
        Assert.Equal('\0', ab.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void StringMatchesSequenceOfTwoCharacters()
    {
        Sequence ab = new(
            new Character('a'),
            new Character('b')
        );

        StringView input = new("abcd");
        var match = ab.Match(input);
        Assert.True(match.Success());
        Assert.Equal('c', match.RemainingText().Peek());
    }
    
    [Fact]
    public void StringDoesNotMatchSequenceOfCharacters()
    {
        Sequence ab = new(
            new Character('a'),
            new Character('b')
        );

        StringView input = new("ax");
        var match = ab.Match(input);
        Assert.False(match.Success());
        Assert.Equal('a', match.RemainingText().Peek());

        StringView secondInput = new("def");
        var secondMatch = ab.Match(secondInput);
        Assert.False(secondMatch.Success());
        Assert.Equal('d',secondMatch.RemainingText().Peek());
    }


    [Fact]
    public void StringMatchesSequenceOfSequenceOfCharacters()
    {
        Sequence ab = new(
            new Character('a'),
            new Character('b')
        );
        
        Sequence abc = new(
            ab,
            new Character('c')
        );

        StringView input = new("abcd");
        var match = abc.Match(input);
        Assert.True(match.Success());
        Assert.Equal('d', match.RemainingText().Peek());
    }

    [Fact]
    public void StringDoesNotMatchSequenceOfSequenceOfCharacters()
    {
        Sequence ab = new(
            new Character('a'),
            new Character('b')
        );
        
        Sequence abc = new(
            ab,
            new Character('c')
        );

        StringView input = new("def");
        Assert.False(abc.Match(input).Success());
        Assert.Equal('d', abc.Match(input).RemainingText().Peek());

        StringView secondInput = new("abx");
        Assert.False(abc.Match(secondInput).Success());
        Assert.Equal('a', abc.Match(secondInput).RemainingText().Peek());
    }

    [Fact]
    public void EmptyStringDoesNotMatchSequenceOfSequenceOfCharacters()
    {
        Sequence ab = new(
            new Character('a'),
            new Character('b')
        );
        
        Sequence abc = new(
            ab,
            new Character('c')
        );

        StringView input = new("");
        Assert.False(abc.Match(input).Success());
        Assert.Equal('\0', abc.Match(input).RemainingText().Peek());
    }

    [Fact]
    public void NullStringDoesNotMatchSequenceOfSequenceOfCharacters()
    {
        Sequence ab = new(
            new Character('a'),
            new Character('b')
        );
        
        Sequence abc = new(
            ab,
            new Character('c')
        );

        StringView input = new(null);
        Assert.False(abc.Match(input).Success());
        Assert.Equal('\0', abc.Match(input).RemainingText().Peek());
    }
    
    [Fact]
    public void ValidInputsMatchesHexadecimalSequence()
    {
        Choice hex = new(
            new Range('0', '9'),
        new Range('a', 'f'),
        new Range('A', 'F')
            );

        Sequence hexSeq = new(
            new Character('u'),
            new Sequence(
                hex,
                hex,
                hex,
                hex
            )
        );
        
        StringView input = new("u1234");
        var match = hexSeq.Match(input);
        Assert.True(match.Success());
        Assert.Equal('\0', match.RemainingText().Peek());

        StringView secondInput = new("uabcdef");
        var secondMatch = hexSeq.Match(secondInput);
        Assert.True(secondMatch.Success());
        Assert.Equal('e', secondMatch.RemainingText().Peek());

        StringView thirdInput = new("uB005 ab");
        var thirdMatch = hexSeq.Match(thirdInput);

        Assert.True(thirdMatch.Success());
        Assert.Equal(' ', thirdMatch.RemainingText().Peek());

        StringView fourthInput = new("abc");
        var fourthMatch = hexSeq.Match(fourthInput);
        Assert.False(fourthMatch.Success());
        Assert.Equal('a', fourthMatch.RemainingText().Peek());
    }

    [Fact]
    public void NullInputDoesNotMatchHexadecimalSequence()
    {
        Choice hex = new(
            new Range('0', '9'),
            new Range('a', 'f'),
            new Range('A', 'F')
        );

        Sequence hexSeq = new(
            new Character('u'),
            new Sequence(
                hex,
                hex,
                hex,
                hex
            )
        );

        StringView input = new(null);
        Assert.False(hexSeq.Match(input).Success());
        Assert.Equal('\0', hexSeq.Match(input).RemainingText().Peek());
    }
}