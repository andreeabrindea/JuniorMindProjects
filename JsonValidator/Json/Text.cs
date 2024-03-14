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
        return text?.StartsWith(prefix) != true ? new FailedMatch(text) : new SuccessMatch(text.Advance(prefix.Length));
    }
}
#pragma warning restore CA1724
