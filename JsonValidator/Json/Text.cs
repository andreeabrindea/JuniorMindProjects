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
        IMatch match = !text.IsEmpty() && text.StartsWith(prefix) == 0
            ? new SuccessMatch(text.Advance(prefix.Length))
            : new FailedMatch(text, text.Advance(prefix.Length - 1));

        Console.WriteLine("Text " + match.Success() + " " + text.StartIndex() + " " + match.RemainingText().StartIndex() + " " + match.Position().StartIndex());
        return match;
    }
}
#pragma warning restore CA1724
