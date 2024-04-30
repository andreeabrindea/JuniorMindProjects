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
        StringView maxMatchPosition = match.Position();
        foreach (var pattern in patterns)
        {
            match = pattern.Match(match.RemainingText());

            if (maxMatchPosition.StartIndex() < match.Position().StartIndex())
            {
                maxMatchPosition = match.Position();
            }

            if (!match.Success())
            {
                return new FailedMatch(text, maxMatchPosition);
            }
        }

        return new SuccessMatch(match.RemainingText(), match.Position());
    }
}
