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
        Console.WriteLine("Many start " + text.StartIndex() + " " + text.Peek());

        IMatch match = new SuccessMatch(text);
        while (match.Success())
        {
            match = pattern.Match(match.RemainingText());
        }

        Console.WriteLine("Many final " + text.StartIndex() + " " + text.Peek());

        return new SuccessMatch(match.RemainingText());
    }
}