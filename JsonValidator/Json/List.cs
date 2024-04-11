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
                    new Sequence(separator, element), "many list")));
    }

    public IMatch Match(StringView text)
    {
        return pattern.Match(text);
    }
}
