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
        return text.IsEmpty() || !accepted.Contains(text.Peek()) ? new FailedMatch(text, text.StartIndex()) : new SuccessMatch(text.Advance());
    }
}