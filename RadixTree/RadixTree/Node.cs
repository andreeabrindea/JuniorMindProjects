using RadixTree;

public class Node {
    internal bool IsLeaf { get; set; }

    internal Dictionary<char, Edge> Edges { get; private set; }

    internal Node(bool isLeaf)
    {
        this.IsLeaf = isLeaf;
        this.Edges = new Dictionary<char, Edge>();
    }
}