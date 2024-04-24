namespace Json;

public class Range : IPattern
{
    private readonly char startCharacter;
    private readonly char endCharacter;

    public Range(char start, char end)
    {
        startCharacter = start;
        endCharacter = end;
    }

    public IMatch Match(StringView text)
    {
        IMatch match = !text.IsEmpty() && char.IsBetween(text.Peek(), startCharacter, endCharacter)
            ? new SuccessMatch(text.Advance())
            : new FailedMatch(text);

        Console.WriteLine("Range " + match.Success() + " " + text.StartIndex() + " " + match.RemainingText().StartIndex() + " " + match.Position().StartIndex());

        return match;
    }
}