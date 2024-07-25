namespace RadixTree;

public class Edge
{
    internal string Value { get; set; }

    internal Node Next { get; set; }

    internal Edge(string value)
    {
        this.Value = value;
        this.Next = new Node(true);
    }

    internal Edge(string value, Node next)
    {
        this.Value = value;
        this.Next = next;
    }
}