namespace Json;

public class Many : IPattern
{
    readonly IPattern pattern;
    readonly string name;

    public Many(IPattern pattern, string name = "")
    {
        this.pattern = pattern;
        this.name = name;
    }

    public IMatch Match(StringView text)
    {
        IMatch match = new SuccessMatch(text);
        while (match.Success())
        {
            match = pattern.Match(text);
            text = match.RemainingText();
        }

        return new SuccessMatch(match.RemainingText(), match.Position());
    }
}