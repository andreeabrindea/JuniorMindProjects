using System.Collections;

namespace Linq;

public class OrderedEnumerable<TSource> : IOrderedEnumerable<TSource>
{
    private readonly List<TSource> source;
    private readonly IComparer<TSource> comparer;

    public OrderedEnumerable(IEnumerable<TSource> source, IComparer<TSource> comparer)
    {
        this.source = source.ToList();
        this.comparer = comparer;
    }

    public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(
        Func<TSource, TKey> keySelector, IComparer<TKey>? comparer, bool descending)
    {
        comparer ??= Comparer<TKey>.Default;
        var secondaryComparer = Comparer<TSource>.Create((x, y) =>
        {
            int result = this.comparer.Compare(x, y);
            if (result != 0)
            {
                return result;
            }

            var keyX = keySelector(x);
            var keyY = keySelector(y);
            return descending ? comparer.Compare(keyY, keyX) : comparer.Compare(keyX, keyY);
        });

        return new OrderedEnumerable<TSource>(source, secondaryComparer);
    }

    public IEnumerator<TSource> GetEnumerator()
    {
        source.Sort(comparer.Compare);
        foreach (var s in source)
        {
            yield return s;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}