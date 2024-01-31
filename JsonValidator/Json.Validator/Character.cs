namespace Json;

public class Character : IPattern
{
    readonly char pattern;

    public Character(char pattern)
    {
        this.pattern = pattern;
    }

    public IMatch Match(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return new FailedMatch(text);
        }

        if (text[0] == pattern)
        {
            return new SuccessMatch(text);
        }

        return new FailedMatch(text);
    }
}