namespace Json;

public class Many : IPattern
{
    readonly IPattern pattern;

    public Many(IPattern pattern)
    {
        this.pattern = pattern;
    }

    public IMatch Match(StringView text)
    {
        IMatch match = pattern.Match(text);
        while (match.Success())
        {
            match = pattern.Match(text);
            text = match.RemainingText();
        }

        if (!match.Success())
        {
            return new SuccessMatch(match.RemainingText(), match.Position());
        }

        return new SuccessMatch(match.RemainingText(), match.Position());
    }
}
