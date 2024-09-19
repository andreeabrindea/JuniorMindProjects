namespace RadixTreeStructure
{
    public class Edge<T>
    {
        internal Edge(List<T> value, Node<T> next)
        {
            this.Value = value;
            this.Next = next;
        }

        internal List<T> Value { get; set; }

        internal Node<T> Next { get; set; }
    }
}