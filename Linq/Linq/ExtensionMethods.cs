namespace Linq;

public static class ExtensionMethods
{
    public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);
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
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);
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
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);
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
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(selector);
        foreach (var s in source)
        {
            yield return selector(s);
        }
    }

    public static IEnumerable<TResult> SelectMany<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, IEnumerable<TResult>> selector)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(selector);

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
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);
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
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(keySelector);
        ArgumentNullException.ThrowIfNull(elementSelector);

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
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);
        ArgumentNullException.ThrowIfNull(resultSelector);
        var firstEnumerator = first.GetEnumerator();
        var secondEnumerator = second.GetEnumerator();
        while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
        {
            yield return resultSelector(firstEnumerator.Current, secondEnumerator.Current);
        }
    }

    public static TAccumulate Aggregate<TSource, TAccumulate>(
        this IEnumerable<TSource> source,
        TAccumulate seed,
        Func<TAccumulate, TSource, TAccumulate> func)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(func);

        var result = seed;
        foreach (var s in source)
        {
            result = func(result, s);
        }

        return result;
    }

    public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter, TInner, TResult> resultSelector)
    {
        ArgumentNullException.ThrowIfNull(outer);
        ArgumentNullException.ThrowIfNull(inner);
        ArgumentNullException.ThrowIfNull(outerKeySelector);
        ArgumentNullException.ThrowIfNull(innerKeySelector);

        Dictionary<TKey, List<TInner>> innerToDictionary = new();
        foreach (var i in inner)
        {
            TKey key = innerKeySelector(i);
            if (!innerToDictionary.ContainsKey(key))
            {
                innerToDictionary[key] = new List<TInner>();
            }

            innerToDictionary[key].Add(i);
        }

        foreach (var o in outer)
        {
            TKey outerKey = outerKeySelector(o);
            if (!innerToDictionary.ContainsKey(outerKey))
            {
                continue;
            }

            foreach (var i in innerToDictionary[outerKey])
            {
                yield return resultSelector(o, i);
            }
        }
    }

    public static IEnumerable<TSource> Distinct<TSource>(
        this IEnumerable<TSource> source,
        IEqualityComparer<TSource> comparer)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(comparer);
        return new HashSet<TSource>(source, comparer);
    }

    public static IEnumerable<TSource> Union<TSource>(
        this IEnumerable<TSource> first,
        IEnumerable<TSource> second,
        IEqualityComparer<TSource> comparer)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);
        ArgumentNullException.ThrowIfNull(comparer);
        HashSet<TSource> distinctElements = new(comparer);
        foreach (var item in first)
        {
            if (distinctElements.Add(item))
            {
                yield return item;
            }
        }

        foreach (var item in second)
        {
            if (distinctElements.Add(item))
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<TSource> Intersect<TSource>(
        this IEnumerable<TSource> first,
        IEnumerable<TSource> second,
        IEqualityComparer<TSource> comparer)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);
        ArgumentNullException.ThrowIfNull(comparer);
        HashSet<TSource> distinctElements = new(comparer);
        foreach (var item in first)
        {
            if (second.Contains(item) && distinctElements.Add(item))
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<TSource> Except<TSource>(
        this IEnumerable<TSource> first,
        IEnumerable<TSource> second,
        IEqualityComparer<TSource> comparer)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);
        ArgumentNullException.ThrowIfNull(comparer);
        foreach (var item in first)
        {
            if (!second.Contains(item))
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TSource, TElement> elementSelector,
        Func<TKey, IEnumerable<TElement>, TResult> resultSelector,
        IEqualityComparer<TKey> comparer)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(keySelector);
        ArgumentNullException.ThrowIfNull(elementSelector);
        ArgumentNullException.ThrowIfNull(resultSelector);

        Dictionary<TKey, List<TElement>> items = new(comparer);
        foreach (var s in source)
        {
            var key = keySelector(s);
            var element = elementSelector(s);
            if (items.ContainsKey(key))
            {
                items[key].Add(element);
            }
            else
            {
                items[key] = new List<TElement>() { element };
            }
        }

        foreach (var i in items)
        {
            yield return resultSelector(i.Key, i.Value);
        }
    }

    public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        IComparer<TKey> comparer)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(keySelector);
        comparer ??= Comparer<TKey>.Default;

        Comparison<TSource> comparison = (x, y) =>
        {
            var keyX = keySelector(x);
            var keyY = keySelector(y);
            return comparer.Compare(keyX, keyY);
        };

        return new OrderedEnumerable<TSource>(source, comparison);
    }

    public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(
        this IOrderedEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        IComparer<TKey> comparer) => source.CreateOrderedEnumerable(keySelector, comparer ?? Comparer<TKey>.Default, false);
}