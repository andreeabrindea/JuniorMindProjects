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
        IMatch match = new SuccessMatch(text);
        while (match.Success())
        {
            text = match.RemainingText();
            match = pattern.Match(text);
        }

        if (!text.IsEmpty())
        {
            return new SuccessMatch(text, match.Position());
        }

        return new SuccessMatch(match.RemainingText(), match.Position());
    }
}