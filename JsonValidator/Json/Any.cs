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
        int position = text.StartIndex();
        return text.IsEmpty() || !accepted.Contains(text.Peek()) ? new FailedMatch(text, position) : new SuccessMatch(text.Advance());
    }
}