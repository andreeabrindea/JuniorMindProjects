#pragma warning disable CA1716

namespace Json;

public class Optional : IPattern
{
    private readonly IPattern pattern;

    public Optional(IPattern pattern)
    {
        this.pattern = pattern;
    }

    public IMatch Match(string text)
    {
        IMatch match = new SuccessMatch(text);
        match = pattern.Match(match.RemainingText());

        return new SuccessMatch(match.RemainingText());
    }
}
#pragma warning restore CA1716
