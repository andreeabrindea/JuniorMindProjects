namespace Json;

public class OneOrMore : IPattern
{
    private readonly IPattern pattern;

    public OneOrMore(IPattern pattern)
    {
        this.pattern = new Sequence(pattern, new Many(pattern));
    }

    public IMatch Match(StringView text)
    {
        var match = pattern.Match(text);

        if (!match.Success())
        {
            return new FailedMatch(text, match.Position().Advance());
        }

        return match;
    }
}
