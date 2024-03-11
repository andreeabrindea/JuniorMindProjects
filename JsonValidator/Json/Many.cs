namespace Json;

public class Many : IPattern
{
    private readonly IPattern pattern;

    public Many(IPattern pattern)
    {
        this.pattern = pattern;
    }

    public IMatch Match(StringView text)
    {
        IMatch match = new SuccessMatch(text);

        while (match.Success())
        {
            match = pattern.Match(match.RemainingText());
        }

        return new SuccessMatch(match.RemainingText());
    }
}