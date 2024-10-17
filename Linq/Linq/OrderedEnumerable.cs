using System.Collections;

namespace Linq;

public class OrderedEnumerable<TSource> : IOrderedEnumerable<TSource>
{
    private readonly List<TSource> source;

    public OrderedEnumerable(IEnumerable<TSource> source, Comparison<TSource> comparison)
    {
        this.source = source.ToList();
        SortComparers.Add(comparison);
    }

    private List<Comparison<TSource>> SortComparers { get; } = new();

    public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(
        Func<TSource, TKey> keySelector, IComparer<TKey>? comparer, bool descending)
    {
        comparer ??= Comparer<TKey>.Default;
        Comparison<TSource> comparison = (x, y) =>
        {
            var keyX = keySelector(x);
            var keyY = keySelector(y);
            return descending ? comparer.Compare(keyY, keyX) : comparer.Compare(keyX, keyY);
        };

        var newOrderedEnumerable = new OrderedEnumerable<TSource>(source, comparison);
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