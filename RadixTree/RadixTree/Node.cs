namespace RadixTreeStructure
{
    public class Node<T>
    {
        public Node(bool isLeaf)
        {
            this.IsLeaf = isLeaf;
            this.Edges = new List<Edge<T>>();
        }

        internal bool IsLeaf { get; set; }

        internal List<Edge<T>> Edges { get; }

        internal void AddEdge(List<T> label, Node<T> next)
        {
            Edges.Add(new Edge<T>(label, next));
        }

        internal Edge<T> GetEdge(List<T> value)
        {
            foreach (var edge in Edges)
            {
                if (edge.Value.Equals(value))
                {
                    return edge;
                }
            }

            return null;
        }

        internal void CopyEdges(List<Edge<T>> edges)
        {
            foreach (var edge in edges)
            {
                AddEdge(edge.Value, edge.Next);
            }
        }
    }
}