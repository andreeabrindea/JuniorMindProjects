namespace Json;

public class Many : IPattern
{
    private readonly IPattern pattern;

    public Many(IPattern pattern)
    {
        this.pattern = pattern;
    }

    public IMatch Match(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return new SuccessMatch(text);
        }

        foreach (var unused in text)
        {
            var match = pattern.Match(text);
            text = match.RemainingText();
        }

        return new SuccessMatch(text);
    }
}