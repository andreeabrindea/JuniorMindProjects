namespace Json;

public class List : IPattern
{
    private readonly IPattern pattern;

    public List(IPattern element, IPattern separator)
    {
        pattern = new Optional(
            new Sequence(
                element,
                new Many(
                    new Sequence(separator, element))));
    }

    public IMatch Match(StringView text)
    {
        var match = pattern.Match(text);
        Console.WriteLine("List " + match.RemainingText().StartIndex() + "  vs  " + match.Position().StartIndex());
        if (!match.Success())
        {
            return new SuccessMatch(text, match.Position());
        }

        return match;
    }
}
