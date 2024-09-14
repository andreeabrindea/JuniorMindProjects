namespace RadixTreeStructure
{
    public class Edge<T>
        where T : struct
    {
        internal Edge(IEnumerable<T> value, Node<T> next)
        {
            this.Value = value;
            this.Next = next;
        }

        internal IEnumerable<T> Value { get; set; }

        internal Node<T> Next { get; set; }
    }
}