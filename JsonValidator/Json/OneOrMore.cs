namespace Json;

public class OneOrMore : IPattern
{
    private readonly IPattern pattern;

    public OneOrMore(IPattern pattern)
    {
        this.pattern = new Sequence(pattern, new Many(pattern, "many oneOrMore"));
    }

    public IMatch Match(StringView text)
    {
        return pattern.Match(text);
    }
}
