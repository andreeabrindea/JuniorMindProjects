#pragma warning disable CA1716, CA1720
namespace Json;
public class String : IPattern
{
    private readonly IPattern pattern;

    public String()
    {
        var charactersAfterQuotesUntilBackslashCharacter = new Range((char)35, (char)91);
        var charactersAfterBackslash = new Range((char)93, char.MaxValue);

        var controlCharacters = new Any("\"\\/bfrtn");
        var digit = new Range('0', '9');
        var hex = new Choice(digit, new Range('a', 'f'), new Range('A', 'F'));
        var completeUnicodeCharacter = new Sequence(new Character('u'), hex, hex, hex, hex);
        var completeControlCharacters = new Choice(controlCharacters, completeUnicodeCharacter);
        var backslashFollowedControlCharacter = new Sequence(new Character('\\'), completeControlCharacters);

        var character = new Choice(new Any(" "), charactersAfterQuotesUntilBackslashCharacter, charactersAfterBackslash, backslashFollowedControlCharacter);
        var characters = new Many(character);

        pattern = new Sequence(new Character('\"'), characters, new Character('\"'));
    }

    public IMatch Match(string text)
    {
        return pattern.Match(text);
    }
}
#pragma warning restore CA1716, CA1720

