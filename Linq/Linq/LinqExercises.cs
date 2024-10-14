namespace Linq;

public class LinqExercises
{
    public static (int, int) GetNoOfConsonantsAndVowels(string s) =>
        (s.Count(character => !"aeiou".Contains(character) && char.IsLetter(character)),
            s.Count(character => "aeiou".Contains(character) && char.IsLetter(character)));

    public static char GetFirstCharacterThatDoesNotRepeat(string s) =>
        s.ToLookup(p => p).First(p => p.Count() == 1).Key;
}