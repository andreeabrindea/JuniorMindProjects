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
        IMatch match = !text.IsEmpty() && text.Peek() == pattern
            ? new SuccessMatch(text.Advance())
            : new FailedMatch(text);

        Console.WriteLine("Character " + match.Success() + " " + text.StartIndex() + " " + match.RemainingText().StartIndex() + " " + match.Position().StartIndex());
        return match;
    }
}