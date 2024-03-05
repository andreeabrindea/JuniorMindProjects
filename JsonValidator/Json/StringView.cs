namespace Json;

public class StringView
{
    private readonly string remainingText;
    private int index;

    public StringView(string remainingText)
    {
        this.index = 0;
        this.remainingText = remainingText;
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
        return new StringView(remainingText.Substring(index));
    }

    public bool StartsWith(string prefix)
    {
        return remainingText.StartsWith(prefix);
    }

    bool IsEmpty()
    {
        return index == remainingText.Length - 1;
    }
}
