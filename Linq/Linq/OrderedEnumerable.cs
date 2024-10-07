using System.Collections;

namespace Linq;

public class OrderedEnumerable<TSource> : IOrderedEnumerable<TSource>
{
    private readonly IEnumerable<TSource> source;

    public OrderedEnumerable(IEnumerable<TSource> source)
    {
        this.source = source;
    }

    public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey>? comparer, bool descending) =>
         new OrderedEnumerable<TSource>(source.OrderBy(keySelector, comparer).ToList());

    public IEnumerator<TSource> GetEnumerator() => source.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}