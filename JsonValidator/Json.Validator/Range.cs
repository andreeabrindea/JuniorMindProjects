namespace Json;

public class Range
{
    private readonly char startCharacter;
    private readonly char endCharacter;

    public Range(char start, char end)
    {
        this.startCharacter = start;
        this.endCharacter = end;
    }

    public bool Match(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return false;
        }

        if (startCharacter > endCharacter)
        {
            return false;
        }

        foreach (char c in text)
        {
            if (c < startCharacter || c > endCharacter)
            {
                return false;
            }
        }

        return true;
    }
}