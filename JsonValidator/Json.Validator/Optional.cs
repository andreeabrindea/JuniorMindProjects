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
        if (pattern.Match(text).Success())
        {
            return new SuccessMatch(text[1..]);
        }

        return new SuccessMatch(text);
    }
}
#pragma warning restore CA1716
