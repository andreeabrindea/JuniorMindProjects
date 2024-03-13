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
        if (text.IsEmpty())
        {
            return new FailedMatch(text);
        }

        if (text.Peek() == pattern)
        {
            var newText = text.Advance();
            return new SuccessMatch(newText);
        }

        return new FailedMatch(text);
    }
}