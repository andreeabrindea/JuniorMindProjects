namespace Json;

public class StringView
{
    private readonly string remainingText;
    private int index;

    public StringView(string remainingText)
    {
        index = 0;
        this.remainingText = remainingText ?? string.Empty;
    }

    public char Peek()
    {
        try
        {
            return string.IsNullOrEmpty(remainingText) ? '\0' : remainingText[index];
        }
        catch (IndexOutOfRangeException e)
        {
            return '\0';
        }
    }

    public StringView Advance(int step = 1)
    {
        return new StringView(remainingText)
        {
            index = index + step
        };
    }

    public bool StartsWith(string prefix)
    {
        return remainingText.StartsWith(prefix);
    }

    public bool IsEmpty()
    {
        return index == remainingText.Length;
    }

    public StringView Remove(string input)
    {
        return new StringView(remainingText.Replace(input, ""));
    }
}