namespace Json;

public class Any : IPattern
{
    private readonly string accepted;
    private readonly string name;

    public Any(string accepted, string name = "")
    {
        this.accepted = accepted;
        this.name = name;
    }

    public IMatch Match(StringView text)
    {
        return text.IsEmpty() || !accepted.Contains(text.Peek()) ?
            new FailedMatch(text) :
            new SuccessMatch(text.Advance());
    }
}