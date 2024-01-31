namespace Json;

public class FailedMatch : IMatch
{
    private readonly string text;

    public FailedMatch(string text)
    {
        this.text = text;
    }

    public bool Success()
    {
        return false;
    }

    public string RemainingText()
    {
        return text;
    }
}