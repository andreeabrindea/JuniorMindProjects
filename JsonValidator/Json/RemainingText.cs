namespace Json;

public class RemainingText
{
    private string remainingText;
    private int index;

    public RemainingText(string remainingText)
    {
        this.index = 0;
        this.remainingText = remainingText;
    }

    public void IncrementIndex()
    {
        index++;
    }

    public string RemoveSubstringFromIndex()
    {
        return !string.IsNullOrEmpty(remainingText) ? remainingText.Substring(index) : remainingText;
    }

    public string GetRemainingText()
    {
        return remainingText;
    }

    public void SetRemainingText(string newText)
    {
        remainingText = newText;
    }
}
