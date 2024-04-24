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
        Console.WriteLine("Many " + text.StartIndex() + " vs " + match.RemainingText().StartIndex() + " vs " + match.Position().StartIndex());
        while (match.Success())
        {
            match = pattern.Match(text);
            text = match.RemainingText();
        }

        Console.WriteLine("Many final " + match.Success() + " " + text.StartIndex() + " vs " + match.RemainingText().StartIndex() + " vs " + match.Position().StartIndex());

        if (!match.Success())
        {
            return new SuccessMatch(match.RemainingText(), match.Position());
        }

        return new SuccessMatch(match.RemainingText(), match.Position());
    }
}
