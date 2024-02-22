#pragma warning disable CA1716, CA1720
namespace Json;

public class String : IPattern
{
    private readonly IPattern pattern;

    public String()
    {
        var completeUnicodeCharacter = new Sequence(
            new Character('u'),
            new Choice(new Range('0', '9'), new Range('a', 'f'), new Range('A', 'F')),
            new Choice(new Range('0', '9'), new Range('a', 'f'), new Range('A', 'F')),
            new Choice(new Range('0', '9'), new Range('a', 'f'), new Range('A', 'F')),
            new Choice(new Range('0', '9'), new Range('a', 'f'), new Range('A', 'F')));

        var character = new Choice(
            new Any(" !"),
            new Range('#', '['),
            new Range(']', char.MaxValue),
            new Sequence(
                new Character('\\'),
                new Choice(new Any("\"\\/bfrtn"), completeUnicodeCharacter)));

        pattern = new Sequence(new Character('\"'), new Many(character), new Character('\"'));
    }

    public IMatch Match(string text)
    {
        return pattern.Match(text);
    }
}
#pragma warning restore CA1716, CA1720

