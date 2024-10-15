using System.Collections;

namespace Linq;

public class OrderedEnumerable<TSource, TKey> : IOrderedEnumerable<TSource>
{
    private readonly List<TSource> source;

    public OrderedEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
    {
        this.source = source.ToList();
        if (descending)
        {
            SortComparers.Add((x, y) => comparer.Compare(keySelector(y), keySelector(x)));
        }
        else
        {
            SortComparers.Add((x, y) => comparer.Compare(keySelector(x), keySelector(y)));
        }
    }

    public List<Func<TSource, TSource, int>> SortComparers { get; } = new();

    public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(
        Func<TSource, TKey> keySelector, IComparer<TKey>? comparer, bool descending)
    {
        var newOrderedEnumerable = new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer, descending);
        newOrderedEnumerable.SortComparers.AddRange(SortComparers);
        return newOrderedEnumerable;
    }

    public IEnumerator<TSource> GetEnumerator()
    {
        source.Sort((x, y) =>
        {
            foreach (var comparer in SortComparers)
            {
                var result = comparer(x, y);
                if (result != 0)
                {
                    return result;
                }
            }

            return 0;
        });

        foreach (var s in source)
        {
            yield return s;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}