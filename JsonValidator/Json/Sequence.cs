namespace Json;

public class Sequence : IPattern
{
    private readonly IPattern[] patterns;

    public Sequence(params IPattern[] patterns)
    {
        this.patterns = patterns;
    }

    public IMatch Match(StringView text)
    {
        IMatch match = new SuccessMatch(text);
        StringView maxMatchPosition = match.Position();
        foreach (var pattern in patterns)
        {
            match = pattern.Match(match.RemainingText());
            Console.WriteLine("INAINTE Sequence " + match.Success() + " " + match.RemainingText().StartIndex() + " " + match.Position().StartIndex());

            if (maxMatchPosition.StartIndex() < match.Position().StartIndex())
            {
                maxMatchPosition = match.Position();
            }

            if (!match.Success())
            {
                Console.WriteLine("aici unsuccess " + match.Position().StartIndex());
                return new FailedMatch(text, maxMatchPosition);
            }

            Console.WriteLine("dupa check " + match.Position().StartIndex());
        }

        Console.WriteLine("aici " + match.Position().StartIndex());
        Console.WriteLine(" sequence maxMatch " + maxMatchPosition.StartIndex());
        return new SuccessMatch(match.RemainingText(), match.Position());
    }
}
