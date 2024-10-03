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

    public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        foreach (var s in source)
        {
            if (predicate(s))
            {
                return s;
            }
        }

        throw new InvalidOperationException();
    }

    public static IEnumerable<TResult> Select<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, TResult> selector)
    {
        foreach (var s in source)
        {
            yield return selector(s);
        }
    }

    public static IEnumerable<TResult> SelectMany<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, IEnumerable<TResult>> selector)
    {
        foreach (var s in source)
        {
            foreach (var item in selector(s))
            {
                yield return item;
            }
        }
    }
}