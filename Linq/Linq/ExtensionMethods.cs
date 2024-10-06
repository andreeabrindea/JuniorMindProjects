namespace Linq;

public static class ExtensionMethods
{
    public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        CheckToThrowException(source);
        CheckToThrowException(predicate);
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
        CheckToThrowException(source);
        CheckToThrowException(predicate);
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
        CheckToThrowException(source);
        CheckToThrowException(predicate);
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
        CheckToThrowException(source);
        CheckToThrowException(selector);
        foreach (var s in source)
        {
            yield return selector(s);
        }
    }

    public static IEnumerable<TResult> SelectMany<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, IEnumerable<TResult>> selector)
    {
        CheckToThrowException(source);
        CheckToThrowException(selector);

        foreach (var s in source)
        {
            foreach (var item in selector(s))
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        CheckToThrowException(source);
        CheckToThrowException(predicate);
        foreach (var s in source)
        {
            if (predicate(s))
            {
                yield return s;
            }
        }
    }

    public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TSource, TElement> elementSelector)
    {
        CheckToThrowException(source);
        CheckToThrowException(keySelector);
        CheckToThrowException(elementSelector);

        Dictionary<TKey, TElement> dictionary = new();
        foreach (var item in source)
        {
            dictionary.Add(keySelector(item), elementSelector(item));
        }

        return dictionary;
    }

    public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(
        this IEnumerable<TFirst> first,
        IEnumerable<TSecond> second,
        Func<TFirst, TSecond, TResult> resultSelector)
    {
        CheckToThrowException(first);
        CheckToThrowException(second);
        CheckToThrowException(resultSelector);
        int length = Math.Min(first.Count(), second.Count());
        for (int i = 0; i < length; i++)
        {
            yield return resultSelector(first.ElementAt(i), second.ElementAt(i));
        }
    }

    public static TAccumulate Aggregate<TSource, TAccumulate>(
        this IEnumerable<TSource> source,
        TAccumulate seed,
        Func<TAccumulate, TSource, TAccumulate> func)
    {
        CheckToThrowException(source);
        CheckToThrowException(func);

        foreach (var s in source)
        {
            seed = func(seed, s);
        }

        return seed;
    }

    public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter, TInner, TResult> resultSelector)
    {
        CheckToThrowException(outerKeySelector);
        CheckToThrowException(innerKeySelector);

        foreach (var o in outer)
        {
            foreach (var i in inner)
            {
                if (outerKeySelector(o).Equals(innerKeySelector(i)))
                {
                    yield return resultSelector(o, i);
                }
            }
        }
    }

    public static IEnumerable<TSource> Distinct<TSource>(
        this IEnumerable<TSource> source,
        IEqualityComparer<TSource> comparer)
    {
        CheckToThrowException(source);
        CheckToThrowException(comparer);
        HashSet<TSource> distinctElements = new(comparer);
        foreach (var s in source)
        {
            if (distinctElements.Add(s))
            {
                yield return s;
            }
        }
    }

    private static void CheckToThrowException<T>(T argument)
    {
        if (argument != null)
        {
            return;
        }

        throw new ArgumentNullException(nameof(argument));
    }
}