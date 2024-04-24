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

        pattern = new Sequence(integer, new Optional(fraction), new Optional(exponent));
    }

    public IMatch Match(StringView text)
    {
        var match = pattern.Match(text);
        Console.WriteLine("Number " + match.Success() + " " + text.StartIndex() + " vs " + match.RemainingText().StartIndex() + " vs " + match.Position().StartIndex());

        if (!match.Success())
        {
            return new FailedMatch(text, match.Position());
        }

        return match;
    }
}
