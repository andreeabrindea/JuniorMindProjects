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
        return !text.IsEmpty() && text.Peek() == pattern
            ? new SuccessMatch(text.Advance())
            : new FailedMatch(text, text.StartIndex());
    }
}