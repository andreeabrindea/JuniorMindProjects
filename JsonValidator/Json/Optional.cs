#pragma warning disable CA1716

namespace Json;

public class Optional : IPattern
{
    private readonly IPattern pattern;

    public Optional(IPattern pattern)
    {
        this.pattern = pattern;
    }

    public IMatch Match(StringView text)
    {
        var match = pattern.Match(text);
        if (!match.Success())
        {
            return new SuccessMatch(text, match.Position());
        }

        return new SuccessMatch(match.RemainingText(), match.Position());
    }
}
#pragma warning restore CA1716