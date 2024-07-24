namespace RadixTree;

public class Edge
{
    internal string Label { get; set; }

    internal Node Next { get; set; }

    internal Edge(string label)
    {
        this.Label = label;
        this.Next = new Node(true);
    }

    internal Edge(string label, Node next)
    {
        this.Label = label;
        this.Next = next;
    }
}