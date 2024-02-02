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

        foreach (var acceptedCharacter in accepted)
        {
            if (text[0] == acceptedCharacter)
            {
                return new SuccessMatch(text[1..]);
            }
        }

        return new FailedMatch(text);
    }
}
