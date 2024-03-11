namespace Json;

public class StringView : ICloneable
{
    private string remainingText;
    private int index;

    public StringView(string remainingText)
    {
        this.index = 0;
        this.remainingText = remainingText ?? string.Empty;
    }

    private StringView()
    {
    }

    public char Peek()
    {
        try
        {
            return remainingText[index];
        }
        catch (IndexOutOfRangeException e)
        {
            return '\0';
        }
    }

    public StringView Advance(int step = 1)
    {
        index += step;
        return new StringView(remainingText[index..]);
    }

    public bool StartsWith(string prefix)
    {
        return remainingText.StartsWith(prefix);
    }

    public bool IsEmpty()
    {
        return index > 0 && remainingText.Length < 1;
    }

    public StringView Remove(string input)
    {
        return new StringView(remainingText.Replace(input, ""));
    }

    public object Clone()
    {
        return new StringView()
        {
            remainingText = remainingText
        };
    }
}
