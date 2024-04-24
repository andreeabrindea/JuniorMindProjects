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
        Console.WriteLine("Character " + text.StartIndex());

        return !text.IsEmpty() && text.Peek() == pattern
            ? new SuccessMatch(text.Advance())
            : new FailedMatch(text);
    }
}