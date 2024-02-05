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

        int index = text.IndexOfAny(accepted.ToCharArray());

        if (index == 0)
        {
            return new SuccessMatch(text[1..]);
        }

        return new FailedMatch(text);
    }
}
