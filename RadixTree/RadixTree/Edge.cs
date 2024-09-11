using System.Collections;

namespace RadixTreeStructure
{
    public class Edge<T>
        where T : IEnumerable
    {
        internal Edge(T value, Node<T> next)
        {
            this.Value = value;
            this.Next = next;
        }

        internal T Value { get; set; }

        internal Node<T> Next { get; set; }
    }
}