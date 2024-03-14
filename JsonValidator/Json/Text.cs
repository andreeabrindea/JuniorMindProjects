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
        return !text.IsEmpty() && text.StartsWith(prefix)
            ? new SuccessMatch(text.Advance(prefix.Length))
            : new FailedMatch(text);
    }
}
#pragma warning restore CA1724
