using System.Collections;

namespace Linq;

public class OrderedEnumerable<TSource, TKey> : IOrderedEnumerable<TSource>
{
    private readonly List<TSource> source;
    private readonly bool descending;

    public OrderedEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
    {
        this.source = source.ToList();
        this.descending = descending;
        if (descending)
        {
            this.source.Sort((x, y) => comparer.Compare(keySelector(y), keySelector(x)));
        }
        else
        {
            this.source.Sort((x, y) => comparer.Compare(keySelector(x), keySelector(y)));
        }
    }

    public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(
        Func<TSource, TKey> keySelector, IComparer<TKey>? comparer, bool descending)
    {
        return new OrderedEnumerable<TSource, TKey>(
            source.OrderBy(keySelector, comparer).ToList(), keySelector, comparer, descending);
    }

    public IEnumerator<TSource> GetEnumerator()
    {
        foreach (var s in source)
        {
            yield return s;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}