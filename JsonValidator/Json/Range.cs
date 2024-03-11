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
        if (text == null)
        {
            return new FailedMatch(text);
        }

        if (text.IsEmpty())
        {
            return new SuccessMatch(text);
        }

        if (text.Peek() >= startCharacter && text.Peek() <= endCharacter)
        {
            return new SuccessMatch(text.Advance());
        }

        return new FailedMatch(text);
    }
}