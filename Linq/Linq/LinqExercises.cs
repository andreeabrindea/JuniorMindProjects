namespace Linq;

public class LinqExercises
{
    public static (int, int) GetNoOfConsonantsAndVowels(string s) =>
        (s.Count(character => !"aeiou".Contains(character) && char.IsLetter(character)),
            s.Count(character => "aeiou".Contains(character) && char.IsLetter(character)));

    public static char GetFirstCharacterThatDoesNotRepeat(string s) =>
        s.ToLookup(p => p).First(p => p.Count() == 1).Key;

    public static int ConvertsStringToInt(string s)
    {
        const int ten = 10;
        var result = s.Aggregate(0, (accumulate, character) =>
        {
            if (character < '0' || character > '9')
            {
                throw new FormatException(nameof(s));
            }

            return accumulate * ten + (character - '0');
        });
        return GetSign(s) * result;
    }

    private static int GetSign(string s)
    {
        int sign = 1;
        if (s[0] == '-')
        {
            sign = -1;
        }

        return sign;
    }
}