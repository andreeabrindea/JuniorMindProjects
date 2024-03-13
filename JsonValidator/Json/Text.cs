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
        if (text?.StartsWith(prefix) != true)
        {
            return new FailedMatch(text);
        }

        return string.IsNullOrEmpty(prefix) ? new SuccessMatch(text) : new SuccessMatch(text.Advance(prefix.Length));
    }
}
#pragma warning restore CA1724
