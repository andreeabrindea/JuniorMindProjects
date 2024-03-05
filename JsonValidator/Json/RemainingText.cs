namespace Json;

public class AuxClass
{
    private readonly string remainingText;
    private int index;

    public AuxClass(string remainingText)
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
        return remainingText.Substring(index);
    }
}
