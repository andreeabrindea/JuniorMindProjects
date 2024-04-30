using System.Runtime.CompilerServices;

namespace Json;

public class StringView
{
    private readonly string text;
    private readonly int startIndex;

    public StringView(string remainingText, int i = 0)
    {
        startIndex = i;
        this.text = remainingText ?? string.Empty;
    }

    public int StartIndex() => startIndex;

    public char Peek()
    {
        return text[startIndex];
    }

    public StringView Advance(int step = 1)
    {
        return new StringView(text, startIndex + step);
    }

    public int StartsWith(string prefix)
    {
        return string.Compare(text, startIndex, prefix, 0, prefix.Length);
    }

    public bool IsEmpty()
    {
        return startIndex >= text.Length;
    }

    public int[] ToColumnRow()
    {
        int line = 1;
        int column = 1;
        int[] location = new int[2];

        for (int i = 0; i < startIndex; i++)
        {
            line = text[i] == '\n' ? line + 1 : line;
            column = text[i] != '\n' ? column + 1 : 1;
        }

        location[0] = line;
        location[1] = column;

        return location;
    }
}