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
        if (text.IsEmpty())
        {
            return new FailedMatch(text);
        }

        if (text.Peek() >= startCharacter && text.Peek() <= endCharacter)
        {
            var newText = text.Advance();
            return new SuccessMatch(newText);
        }

        return new FailedMatch(text);
    }
}