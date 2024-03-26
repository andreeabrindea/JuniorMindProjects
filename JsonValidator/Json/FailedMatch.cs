namespace Json;

public class FailedMatch : IMatch
{
    private readonly StringView text;
    private readonly int position;

    public FailedMatch(StringView text, int position)
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

    public int Position()
    {
        return position;
    }
}