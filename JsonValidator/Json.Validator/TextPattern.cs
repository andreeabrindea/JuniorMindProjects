namespace Json;

public class TextPattern : IPattern
{
    private readonly string prefix;

    public TextPattern(string prefix)
    {
        this.prefix = prefix;
    }

    public IMatch Match(string text)
    {
        if (text == null)
        {
            return new FailedMatch(text);
        }

        if (!text.StartsWith(prefix))
        {
            return new FailedMatch(text);
        }

        if (!string.IsNullOrEmpty(prefix))
        {
            text = text.Replace(prefix, "");
        }

        return new SuccessMatch(text);
    }
}