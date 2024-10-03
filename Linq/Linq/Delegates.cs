namespace Linq;

public static class Delegates
{
    public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        foreach (var s in source)
        {
            if (!predicate(s))
            {
                return false;
            }
        }

        return true;
    }

    public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        foreach (var s in source)
        {
            if (predicate(s))
            {
                return true;
            }
        }

        return false;
    }
}