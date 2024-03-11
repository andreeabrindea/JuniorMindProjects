namespace Json;

public class Character : IPattern
{
    readonly char pattern;

    public Character(char pattern)
    {
        this.pattern = pattern;
    }

    public IMatch Match(StringView text)
    {
        if (text == null)
        {
            return new FailedMatch(text);
        }

        if (text.IsEmpty())
        {
            return new SuccessMatch(text);
        }

        if (text.Peek() == pattern)
        {
            return new SuccessMatch(text.Advance());
        }

        return new FailedMatch(text);
    }
}