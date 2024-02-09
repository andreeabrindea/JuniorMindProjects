namespace Json;

public class List : IPattern
{
    private readonly IPattern pattern;

    public List(IPattern element, IPattern separator)
    {
        this.pattern = new Many(new Sequence(element, new Optional(separator)));
    }

    public IMatch Match(string text)
    {
        return pattern.Match(text);
    }
}
