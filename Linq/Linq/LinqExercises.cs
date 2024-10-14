namespace Linq;

public class LinqExercises
{
    public static (int, int) GetNoOfConsonantsAndVowels(string s) =>
        (s.Count(character => !"aeiou".Contains(character) && char.IsLetter(character)),
            s.Count(character => "aeiou".Contains(character) && char.IsLetter(character)));
}