namespace Json;

public class FailedMatch : IMatch
{
    private readonly StringView text;
    private readonly StringView position;

    public FailedMatch(StringView text, StringView position)
    {
        this.text = text;
        this.position = position;
    }

    public bool Success()
    {
        return false;
    }

    public StringView RemainingText()
    {
        return text;
    }

    public StringView Position()
    {
        return position;
    }
}