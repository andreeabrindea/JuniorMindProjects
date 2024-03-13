namespace Json;

public class StringView
{
    private readonly string remainingText;
    private readonly int index;

    public StringView(string remainingText, int i = 0)
    {
        index = i;
        this.remainingText = remainingText ?? string.Empty;
    }

    public char Peek()
    {
        return remainingText[index];
    }

    public StringView Advance(int step = 1)
    {
        return new StringView(remainingText, index + step);
    }

    public bool StartsWith(string prefix)
    {
        return remainingText.StartsWith(prefix);
    }

    public bool IsEmpty()
    {
        return index >= remainingText.Length || string.IsNullOrEmpty(remainingText);
    }
}