namespace Json;

public class Sequence : IPattern
{
    private readonly IPattern[] patterns;

    public Sequence(params IPattern[] patterns)
    {
        this.patterns = patterns;
    }

    public IMatch Match(string text)
    {
        IMatch match;
        string initialText = text;
        foreach (var pattern in patterns)
        {
            match = pattern.Match(text);

            if (!match.Success())
            {
                return new FailedMatch(initialText);
            }

            text = match.RemainingText();
        }

        return new SuccessMatch(text);
    }
}
