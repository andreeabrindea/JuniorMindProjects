namespace Json;

public class FailedMatch : IMatch
{
    private readonly StringView text;

    public FailedMatch(StringView text)
    {
        this.text = text;
    }

    public bool Success()
    {
        return false;
    }

    public StringView RemainingText()
    {
        return text;
    }
}