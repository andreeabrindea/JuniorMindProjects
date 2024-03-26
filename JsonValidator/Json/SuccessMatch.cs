namespace Json;

public class SuccessMatch : IMatch
{
    private readonly StringView text;

    public SuccessMatch(StringView text)
    {
        this.text = text;
    }

    public bool Success()
    {
        return true;
    }

    public StringView RemainingText()
    {
        return text;
    }

    public int Position()
    {
        return text.StartIndex();
    }
}
