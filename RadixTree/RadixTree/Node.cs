namespace RadixTreeStructure
{
    public class Node
    {
        public Node(bool isLeaf)
        {
            this.IsLeaf = isLeaf;
            this.Edges = new List<Edge>();
        }

        internal bool IsLeaf { get; set; }

        internal List<Edge> Edges { get; }

        public void AddEdge(string label, Node next)
        {
            Edges.Add(new Edge(label, next));
        }

        internal int NoOfEdges()
        {
            return Edges.Count;
        }
    }
}