using System.Linq;

namespace Json;

public class Any : IPattern
{
    private readonly string accepted;

    public Any(string accepted)
    {
        this.accepted = accepted;
    }

    public IMatch Match(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return new FailedMatch(text);
        }

        if (accepted.Contains(text[0]))
        {
            return new SuccessMatch(text[1..]);
        }

        return new FailedMatch(text);
    }
}
