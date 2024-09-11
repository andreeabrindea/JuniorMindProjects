using System.Collections;

namespace RadixTreeStructure
{
    public class Node<T>
        where T : IEnumerable
    {
        public Node(bool isLeaf)
        {
            this.IsLeaf = isLeaf;
            this.Edges = new List<Edge<T>>();
        }

        internal bool IsLeaf { get; set; }

        internal List<Edge<T>> Edges { get; }

        internal void AddEdge(T label, Node<T> next)
        {
            Edges.Add(new Edge<T>(label, next));
        }
    }
}