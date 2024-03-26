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
        return !text.IsEmpty() && char.IsBetween(text.Peek(), startCharacter, endCharacter)
            ? new SuccessMatch(text.Advance())
            : new FailedMatch(text, text.StartIndex());
    }
}