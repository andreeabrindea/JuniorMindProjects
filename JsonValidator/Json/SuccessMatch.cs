namespace Json;

public class SuccessMatch : IMatch
{
    private readonly StringView text;
    private readonly StringView position;

    public SuccessMatch(StringView text, StringView position = null)
    {
        this.text = text;
        this.position = position ?? text;
    }

    public bool Success()
    {
        return true;
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
