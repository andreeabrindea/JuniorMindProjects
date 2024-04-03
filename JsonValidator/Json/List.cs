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
        Console.WriteLine("List " + text.StartIndex() + " " + text.Peek());
        return pattern.Match(text);
    }
}
