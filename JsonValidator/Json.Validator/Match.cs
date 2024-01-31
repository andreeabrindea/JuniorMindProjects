using System.Net.Mime;

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