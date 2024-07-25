using RadixTree;

public class Node {
    internal bool IsLeaf { get; set; }

    internal Dictionary<char, Edge> Edges { get; }

    internal Node(bool isLeaf)
    {
        this.IsLeaf = isLeaf;
        this.Edges = new Dictionary<char, Edge>();
    }

    internal void AddEdge(string label, Node next)
    {
        Edges[label[0]] = new Edge(label, next);
    }

    internal Edge GetEdgeStringValue(char transitionChar)
    {
        Edges.TryGetValue(transitionChar, out Edge edge);
        return edge;
    }

    internal int NoOfEdges()
    {
        return Edges.Count;
    }
}