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

    public int StartsWith(string prefix)
    {
        return string.Compare(text, startIndex, prefix, 0, prefix.Length);
    }

    public bool IsEmpty()
    {
        return startIndex >= text.Length;
    }
}