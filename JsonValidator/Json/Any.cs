namespace Json;

public class Any : IPattern
{
    private readonly string accepted;

    public Any(string accepted)
    {
        this.accepted = accepted;
    }

    public IMatch Match(StringView text)
    {
        IMatch match = text.IsEmpty() || !accepted.Contains(text.Peek()) ?
            new FailedMatch(text) :
            new SuccessMatch(text.Advance());

        Console.WriteLine("Any " + match.Success() + " " + match.RemainingText().StartIndex() + " " + match.Position().StartIndex());
        return match;
    }
}