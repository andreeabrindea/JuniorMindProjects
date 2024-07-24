using RadixTree;

public class Node {
    internal bool IsLeaf { get; set; }

    internal Dictionary<char, Edge> Edges { get; private set; }

    internal Node(bool isLeaf)
    {
        this.IsLeaf = isLeaf;
        this.Edges = new Dictionary<char, Edge>();
    }

    public void AddEdge(string label, Node next)
    {
        Edges[label[0]] = new Edge(label, next);
    }

    public Edge GetTransition(char transitionChar)
    {
        Edges.TryGetValue(transitionChar, out Edge edge);
        return edge;
    }
}