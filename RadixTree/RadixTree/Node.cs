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

        public Edge GetEdgeStartingWith(string transitionString, out string edgeValue)
        {
            foreach (var edge in Edges)
            {
                if (edge.Value.StartsWith(transitionString))
                {
                    edgeValue = edge.Value;
                    return edge;
                }
            }

            edgeValue = string.Empty;
            return null;
        }

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