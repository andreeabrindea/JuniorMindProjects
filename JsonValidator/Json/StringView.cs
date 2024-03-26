namespace Json;

public class StringView
{
    private readonly string text;
    private readonly int startIndex;

    public StringView(string remainingText, int i = 0)
    {
        startIndex = i;
        this.text = remainingText ?? string.Empty;
    }

    public int StartIndex() => startIndex;

    public char Peek()
    {
        return text[startIndex];
    }

    public StringView Advance(int step = 1)
    {
        return new StringView(text, startIndex + step);
    }

    public bool StartsWith(string prefix)
    {
        return text.StartsWith(prefix);
    }

    public bool IsEmpty()
    {
        return startIndex >= text.Length;
    }
}