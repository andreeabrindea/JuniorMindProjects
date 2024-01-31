namespace Json;

public class Range : IPattern
{
    private readonly char startCharacter;
    private readonly char endCharacter;

    public Range(char start, char end)
    {
        this.startCharacter = start;
        this.endCharacter = end;
    }

    public IMatch Match(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return new Match(false, text);
        }

        return new Match(text[0] >= startCharacter && text[0] <= endCharacter, text);
    }
}