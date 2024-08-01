namespace RadixTreeStructure
{
    public class Edge
    {
        internal Edge(string value, Node next)
        {
            this.Value = value;
            this.Next = next;
        }

        internal string Value { get; set; }

        internal Node Next { get; set; }
    }
}