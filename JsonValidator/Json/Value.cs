namespace Json;

public class Value : IPattern
{
    private readonly IPattern pattern;

    public Value()
    {
        var ws = new Many(new Any(" \n\r\t"));
        var value = new Choice(
            new String(),
            new Number(),
            new Text("true"),
            new Text("false"),
            new Text("null"));

        var element = new Sequence(ws, value, ws);

        var array = new Sequence(
            new Character('['),
            ws,
            new List(element, new Sequence(new Character(','))),
            ws,
            new Character(']'));
        value.Add(array);

        var member = new Sequence(
            ws,
            new String(),
            ws,
            new Character(':'),
            element);

        var objectPattern = new Sequence(
            new Character('{'),
            ws,
            new List(
                member,
                new Character(',')),
            ws,
            new Character('}'));
        value.Add(objectPattern);

        pattern = element;
    }

    public IMatch Match(StringView text)
    {
        Console.WriteLine("Value " + text.StartIndex() + " " + text.Peek());
        return pattern.Match(text);
    }
}
