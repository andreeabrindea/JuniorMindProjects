namespace Json;

public class Match : IMatch
{
    private readonly bool success;
    private readonly string text;

    public Match(bool success, string text)
    {
        this.success = success;
        this.text = text;
    }

    public bool Success()
    {
        return success;
    }

    public string RemainingText()
    {
        if (text.Length < 2)
        {
            return "";
        }

        return text[1..];
    }
}