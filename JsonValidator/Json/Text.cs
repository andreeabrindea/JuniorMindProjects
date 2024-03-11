#pragma warning disable CA1724
namespace Json;

public class Text : IPattern
{
    private readonly string prefix;

    public Text(string prefix)
    {
        this.prefix = prefix;
    }

    public IMatch Match(StringView text)
    {
        if (text == null)
        {
            return new FailedMatch(null);
        }

        if (!text.StartsWith(prefix))
        {
            return new FailedMatch(text);
        }

        if (!string.IsNullOrEmpty(prefix))
        {
            text = text.Remove(prefix);
        }

        return new SuccessMatch(text);
    }
}
#pragma warning restore CA1724
