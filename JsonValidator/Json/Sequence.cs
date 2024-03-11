namespace Json;

public class Sequence : IPattern
{
    private readonly IPattern[] patterns;

    public Sequence(params IPattern[] patterns)
    {
        this.patterns = patterns;
    }

    public IMatch Match(StringView text)
    {
        IMatch match = new SuccessMatch(text);
        StringView initialText = (StringView)text.Clone();
        foreach (var pattern in patterns)
        {
            match = pattern.Match(match.RemainingText());

            if (text.IsEmpty())
            {
                return new SuccessMatch(text);
            }

            if (!match.Success())
            {
                return new FailedMatch(initialText);
            }
        }

        return match;
    }
}
