namespace Json;

public class Number : IPattern
{
    private readonly IPattern pattern;

    public Number()
    {
        var sign = new Optional(new Any("-+"));
        var oneNine = new Range('1', '9');
        var digit = new Choice("Choice digit", new Character('0'), oneNine);
        var digits = new OneOrMore(digit);

        var integer = new Sequence(sign, new Choice("Choice integer", new Sequence(oneNine, digits), digit));
        var fraction = new Sequence(new Character('.'), digits);
        var exponent = new Sequence(new Any("eE"), sign, digits);

        pattern = new Sequence(integer, new Optional(fraction), new Optional(exponent));
    }

    public IMatch Match(StringView text)
    {
        return pattern.Match(text);
    }
}
