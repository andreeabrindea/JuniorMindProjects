using Microsoft.CodeAnalysis.Operations;

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
        var match = pattern.Match(text);

        if (!match.Success())
        {
            return new FailedMatch(text);
        }

        while (match.Success())
        {
            text = match.RemainingText();
            match = pattern.Match(text);
        }

        return new SuccessMatch(text);
    }
}
