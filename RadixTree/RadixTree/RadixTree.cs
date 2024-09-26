using System.Collections;

namespace RadixTreeStructure
{
    public class RadixTree<T> : IEnumerable<IEnumerable<T>>
    where T : IEquatable<T>
    {
        private readonly Node<T> root;

        public RadixTree()
        {
            this.root = new Node<T>(false);
        }

        public IEnumerator<IEnumerable<T>> GetEnumerator() => GetEdges(this.root).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(IEnumerable<T> enumeration) => Add(root, enumeration.ToList(), enumeration.ToList());

        public bool Search(IEnumerable<T> enumeration) => Search(root, enumeration.ToList());

        public void Remove(IEnumerable<T> enumeration) => Remove(root, enumeration.ToList());

        private void Add(Node<T> node, List<T> enumeration, List<T> remainingEnumeration)
        {
            if (enumeration.Count == 0)
            {
                return;
            }

            foreach (var edge in node.Edges)
            {
                if (edge.Value.Equals(remainingEnumeration))
                {
                    return;
                }

                int mismatchIndex = GetMismatchIndex(enumeration, edge.Value);
                if (mismatchIndex == 0)
                {
                    continue;
                }

                if (edge.Value.Count > mismatchIndex)
                {
                    SplitEdgeAndAddRemainder(edge, enumeration, mismatchIndex);
                    node.IsLeaf = false;
                    return;
                }

                if (enumeration.Count > mismatchIndex)
                {
                    AddRemainingEnumeration(edge, enumeration, mismatchIndex);
                    return;
                }

                Add(edge.Next, enumeration, Slice(remainingEnumeration, mismatchIndex, remainingEnumeration.Count));
            }

            root.AddEdge(enumeration, new Node<T>(true));
        }

        private void SplitEdgeAndAddRemainder(Edge<T> edge, List<T> enumeration, int mismatchIndex)
        {
            var commonPrefix = Slice(edge.Value, 0, mismatchIndex);
            var suffixEdge = Slice(edge.Value, mismatchIndex, edge.Value.Count);
            edge.Value = commonPrefix;

            List<Edge<T>> previousEdges = new List<Edge<T>>();
            foreach (var prevEdge in edge.Next.Edges)
            {
                previousEdges.Add(new Edge<T>(prevEdge.Value, prevEdge.Next));
            }

            edge.Next.Edges.Clear();
            edge.Next.AddEdge(suffixEdge, new Node<T>(true));

            if (mismatchIndex < enumeration.Count)
            {
                edge.Next.AddEdge(Slice(enumeration, mismatchIndex, enumeration.Count), new Node<T>(true));
            }

            Edge<T> splitEdge = edge.Next.GetEdge(suffixEdge);
            splitEdge.Next.CopyEdges(previousEdges);
        }

        private void AddRemainingEnumeration(Edge<T> edge, List<T> enumeration, int mismatchIndex) =>
            edge.Next.AddEdge(Slice(enumeration, mismatchIndex, enumeration.Count), new Node<T>(true));

        private bool Search(Node<T> node, List<T> enumeration)
        {
            if (enumeration.Count == 0)
            {
                return false;
            }

            foreach (var edge in node.Edges)
            {
                int mismatchIndex = GetMismatchIndex(enumeration, edge.Value);
                if (mismatchIndex == edge.Value.Count && edge.Value.Count == enumeration.Count)
                {
                    return true;
                }

                enumeration = Slice(enumeration, mismatchIndex, enumeration.Count);
                Search(edge.Next, enumeration);
            }

            return false;
        }

        private void Remove(Node<T> node, List<T> enumeration)
        {
            if (enumeration.Count == 0)
            {
                return;
            }

            foreach (var edge in node.Edges)
            {
                int mismatchIndex = GetMismatchIndex(enumeration, edge.Value);
                if (mismatchIndex == edge.Value.Count && mismatchIndex == enumeration.Count)
                {
                    if (edge.Next.IsLeaf)
                    {
                        edge.Next.IsLeaf = false;
                    }

                    if (edge.Next.Edges.Count == 0)
                    {
                        node.Edges.Remove(edge);
                    }

                    return;
                }

                enumeration = Slice(enumeration, mismatchIndex, enumeration.Count);
                Remove(edge.Next, enumeration);
            }
        }

        private int GetMismatchIndex(List<T> enumeration, List<T> edgeWord)
        {
            int length = Math.Min(enumeration.Count, edgeWord.Count);
            for (int i = 0; i < length; i++)
            {
                if (!enumeration[i].Equals(edgeWord[i]))
                {
                    return i;
                }
            }

            return length;
        }

        private List<T> Slice(List<T> source, int start, int end)
        {
            List<T> result = new List<T>();
            for (int i = start; i < end; i++)
            {
                result.Add(source[i]);
            }

            return result;
        }

        private IEnumerable<IEnumerable<T>> GetEdges(Node<T> node)
        {
            if (node == null)
            {
                yield break;
            }

            foreach (var edge in node.Edges)
            {
                yield return edge.Value;
                foreach (var value in GetEdges(edge.Next))
                {
                    yield return value;
                }
            }
        }
    }
}
