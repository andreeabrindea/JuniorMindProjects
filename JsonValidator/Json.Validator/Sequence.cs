namespace Json;

public class Sequence : IPattern
{
    private readonly IPattern[] patterns;

    public Sequence(params IPattern[] patterns)
    {
        this.patterns = patterns;
    }

    public IMatch Match(string text)
    {
        string inputText = text;
        foreach (var pattern in patterns)
        {
            var match = pattern.Match(inputText);

            if (!match.Success())
            {
                return new FailedMatch(text);
            }

            inputText = match.RemainingText();
        }

        return new SuccessMatch(inputText);
    }
}
