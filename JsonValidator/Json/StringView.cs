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

    public StringView(int i, string remainingText)
    {
        this.index = i;
        this.remainingText = remainingText ?? string.Empty;
    }

    private StringView()
    {
    }

    public void SetIndex(int i)
    {
        this.index = i;
    }

    public char Peek()
    {
        try
        {
            if (string.IsNullOrEmpty(remainingText))
            {
                return '\0';
            }

            return remainingText[index];
        }
        catch (IndexOutOfRangeException e)
        {
            return '\0';
        }
    }

    public StringView Advance(int step = 1)
    {
        var newView = new StringView(remainingText);
        newView.SetIndex(index + step);
        return newView;
    }

    public bool StartsWith(string prefix)
    {
        return remainingText.StartsWith(prefix);
    }

    public bool IsEmpty()
    {
        return index != 0 && index == remainingText.Length;
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