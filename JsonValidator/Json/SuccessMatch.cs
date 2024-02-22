namespace Json;

public class SuccessMatch : IMatch
{
    private readonly string text;

    public SuccessMatch(string text)
    {
        this.text = text;
    }

    public bool Success()
    {
        return true;
    }

    public string RemainingText()
    {
        return text;
    }
}
