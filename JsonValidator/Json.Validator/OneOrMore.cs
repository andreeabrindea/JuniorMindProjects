namespace Json;

public class OneOrMore : IPattern
{
    private readonly IPattern pattern;

    public OneOrMore(IPattern pattern)
    {
        this.pattern = pattern;
    }

    public IMatch Match(string text)
    {
        IMatch match = pattern.Match(text);

        if (!match.Success())
        {
            return new FailedMatch(text);
        }

        var manyPattern = new Many(pattern);
        return manyPattern.Match(match.RemainingText());
    }
}
