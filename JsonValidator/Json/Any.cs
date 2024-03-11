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
        if (text == null)
        {
            return new FailedMatch(text);
        }

        if (accepted.Contains(text.Peek()))
        {
            return new SuccessMatch(text.Advance());
        }

        return new FailedMatch(text);
    }
}