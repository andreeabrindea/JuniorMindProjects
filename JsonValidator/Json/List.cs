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
        Console.WriteLine("List " + match.Success() + " " + text.StartIndex() + " vs " + match.RemainingText().StartIndex() + " vs " + match.Position().StartIndex());

        if (!match.Success())
        {
            Console.WriteLine("List failed " + text.StartIndex() + " vs " + match.RemainingText().StartIndex() + " vs " + match.Position().StartIndex());
            return new FailedMatch(text, match.Position());
        }

        return new SuccessMatch(match.RemainingText(), match.Position());
    }
}
