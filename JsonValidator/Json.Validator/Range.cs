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

    public IMatch Match(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return new FailedMatch(text);
        }

        if (text[0] >= startCharacter && text[0] <= endCharacter)
        {
            return new SuccessMatch(text[1..]);
        }

        return new FailedMatch(text);
    }
}