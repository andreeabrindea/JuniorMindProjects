using System;

namespace Json;
public class Number : IPattern
{
    private readonly IPattern pattern;

    public Number()
    {
        var sign = new Optional(new Any("-+"));
        var oneNine = new Range('1', '9');
        var digit = new Choice(new Character('0'), oneNine);
        var digits = new OneOrMore(digit);

        var integer = new Sequence(sign, new Choice(new Sequence(oneNine, digits), digit));
        var fraction = new Sequence(new Character('.'), digits);
        var exponent = new Sequence(new Any("eE"), sign, digits);

        this.pattern = new Sequence(integer, new Optional(fraction), new Optional(exponent));
    }

    public IMatch Match(string text)
    {
        var match = pattern.Match(text);
        return pattern.Match(text).RemainingText() != string.Empty ? new FailedMatch(match.RemainingText()) : match;
    }
}
