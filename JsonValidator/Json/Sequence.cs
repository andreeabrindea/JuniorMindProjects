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

        foreach (var pattern in patterns)
        {
            match = pattern.Match(match.RemainingText());

            if (!match.Success())
            {
                return new FailedMatch(text, match.Position());
            }
        }

        return match;
    }
}
