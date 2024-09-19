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

        public void Add(IEnumerable<T> enumeration) => Add(root, enumeration, enumeration);

        public bool Search(IEnumerable<T> enumeration) => Search(root, enumeration);

        public bool Remove(IEnumerable<T> enumeration) => Remove(root, enumeration);

        private void Add(Node<T> node, IEnumerable<T> enumeration, IEnumerable<T> remainingEnumeration)
        {
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

                if (edge.Value.ToList().Count > mismatchIndex)
                {
                    SplitEdgeAndAddRemainder(edge, enumeration, mismatchIndex);
                    return;
                }

                if (enumeration.ToList().Count > mismatchIndex)
                {
                    AddRemainingEnumeration(edge, enumeration, mismatchIndex);
                    return;
                }

                Add(edge.Next, enumeration, Slice(remainingEnumeration, mismatchIndex, remainingEnumeration.ToList().Count));
            }

            root.AddEdge(enumeration, new Node<T>(true));
        }

        private void SplitEdgeAndAddRemainder(Edge<T> edge, IEnumerable<T> enumeration, int mismatchIndex)
        {
            var commonPrefix = Slice(edge.Value, 0, mismatchIndex);
            var suffixEdge = Slice(edge.Value, mismatchIndex, edge.Value.ToList().Count);
            edge.Value = commonPrefix;

            List<Edge<T>> previousEdges = new List<Edge<T>>();
            foreach (var prevEdge in edge.Next.Edges)
            {
                previousEdges.Add(new Edge<T>(prevEdge.Value, prevEdge.Next));
            }

            edge.Next.Edges.Clear();
            edge.Next.AddEdge(suffixEdge, new Node<T>(true));

            if (mismatchIndex < enumeration.ToList().Count)
            {
                edge.Next.AddEdge(Slice(enumeration, mismatchIndex, enumeration.ToList().Count), new Node<T>(true));
            }

            Edge<T> splitEdge = edge.Next.GetEdge(suffixEdge);
            splitEdge.Next.CopyEdges(previousEdges);
        }

        private void AddRemainingEnumeration(Edge<T> edge, IEnumerable<T> enumeration, int mismatchIndex) =>
            edge.Next.AddEdge(Slice(enumeration, mismatchIndex, enumeration.ToList().Count), new Node<T>(true));

        private bool Search(Node<T> node, IEnumerable<T> enumeration)
        {
            foreach (var edge in node.Edges)
            {
                int mismatchIndex = GetMismatchIndex(enumeration, edge.Value);
                if (mismatchIndex == edge.Value.ToList().Count && edge.Value.ToList().Count == enumeration.ToList().Count)
                {
                    return true;
                }

                enumeration = Slice(enumeration, mismatchIndex, enumeration.ToList().Count);
                Search(edge.Next, enumeration);
            }

            return false;
        }

        private bool Remove(Node<T> node, IEnumerable<T> enumeration)
        {
            if (!Search(node, enumeration))
            {
                return false;
            }

            foreach (var edge in node.Edges)
            {
                int mismatchIndex = GetMismatchIndex(enumeration, edge.Value);
                if (mismatchIndex == edge.Value.ToList().Count && mismatchIndex == enumeration.ToList().Count)
                {
                    node.Edges.Remove(edge);
                    return true;
                }

                if (mismatchIndex > 0 && node.IsLeaf)
                {
                    node.Edges.Remove(edge);
                    return true;
                }

                enumeration = Slice(enumeration, mismatchIndex, enumeration.ToList().Count);
                Remove(edge.Next, enumeration);
            }

            return false;
        }

        private int GetMismatchIndex(IEnumerable<T> enumeration, IEnumerable<T> edgeWord)
        {
            int length = Math.Min(enumeration.ToList().Count, edgeWord.ToList().Count);
            for (int i = 0; i < length; i++)
            {
                if (!GetElementAtIndex(enumeration, i).Equals(GetElementAtIndex(edgeWord, i)))
                {
                    return i;
                }
            }

            return length;
        }

        private IEnumerable<T> Slice(IEnumerable<T> source, int start, int end) => source.Skip(start).Take(end);

        private object GetElementAtIndex(IEnumerable<T> source, int index) => source.ElementAt(index);

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
