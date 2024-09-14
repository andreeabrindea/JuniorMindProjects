using System.Collections;

namespace RadixTreeStructure
{
    public class RadixTree<T> : IEnumerable<IEnumerable<T>>
        where T : struct
    {
        private readonly Node<T> root;

        public RadixTree()
        {
            this.root = new Node<T>(false);
        }

        public IEnumerator<IEnumerable<T>> GetEnumerator() => GetWords(this.root).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(IEnumerable<T> item) => Add(root, item);

        public bool Search(IEnumerable<T> item) => Search(root, item);

        public bool Remove(IEnumerable<T> item) => Remove(root, item);

        private void Add(Node<T> node, IEnumerable<T> word)
        {
            if (Search(node, word))
            {
                return;
            }

            foreach (var edge in node.Edges)
            {
                int mismatchIndex = GetFirstMismatchLetterIndex(word, edge.Value);
                if (mismatchIndex == 0)
                {
                    break;
                }

                if (Count(edge.Value) > mismatchIndex)
                {
                    var commonPrefix = Slice(edge.Value, 0, mismatchIndex);
                    var suffixEdge = Slice(edge.Value, mismatchIndex, Count(edge.Value));
                    edge.Value = commonPrefix;

                    List<Edge<T>> previousEdges = new List<Edge<T>>();
                    foreach (var prevEdge in edge.Next.Edges)
                    {
                        previousEdges.Add(new Edge<T>(prevEdge.Value, prevEdge.Next));
                    }

                    edge.Next.Edges.Clear();
                    edge.Next.AddEdge(suffixEdge, new Node<T>(true));

                    if (mismatchIndex < Count(word))
                    {
                        edge.Next.AddEdge(Slice(word, mismatchIndex, Count(word)), new Node<T>(true));
                    }

                    Edge<T> splitEdge = edge.Next.GetEdge(suffixEdge);
                    splitEdge.Next.CopyEdges(previousEdges);

                    return;
                }

                if (Count(word) - mismatchIndex > 0)
                {
                    edge.Next.AddEdge(Slice(word, mismatchIndex, Count(word)), new Node<T>(true));
                    return;
                }

                Add(edge.Next, word);
            }

            root.AddEdge(word, new Node<T>(true));
        }

        private bool Search(Node<T> node, IEnumerable<T> word)
        {
            foreach (var edge in node.Edges)
            {
                int mismatchIndex = GetFirstMismatchLetterIndex(word, edge.Value);
                if (mismatchIndex == Count(edge.Value) && Count(edge.Value) == Count(word))
                {
                    return true;
                }

                word = Slice(word, mismatchIndex, Count(word));
                Search(edge.Next, word);
            }

            return false;
        }

        private bool Remove(Node<T> node, IEnumerable<T> word)
        {
            if (!Search(node, word))
            {
                return false;
            }

            foreach (var edge in node.Edges)
            {
                int mismatchIndex = GetFirstMismatchLetterIndex(word, edge.Value);
                if (mismatchIndex == Count(edge.Value) && mismatchIndex == Count(word))
                {
                    node.Edges.Remove(edge);
                    return true;
                }

                if (mismatchIndex > 0 && node.IsLeaf)
                {
                    node.Edges.Remove(edge);
                    return true;
                }

                word = Slice(word, mismatchIndex, Count(word));
                Remove(edge.Next, word);
            }

            return false;
        }

        private int GetFirstMismatchLetterIndex(IEnumerable<T> word, IEnumerable<T> edgeWord)
        {
            int length = Math.Min(Count(word), Count(edgeWord));
            for (int i = 0; i < length; i++)
            {
                if (!GetElementAtIndex(word, i).Equals(GetElementAtIndex(edgeWord, i)))
                {
                    return i;
                }
            }

            return length;
        }

        private IEnumerable<T> Slice(IEnumerable<T> source, int start, int end) => source.Skip(start).Take(end);

        private int Count(IEnumerable<T> source)
        {
            int index = 0;
            foreach (var item in source)
            {
                index++;
            }

            return index;
        }

        private object GetElementAtIndex(IEnumerable<T> source, int index)
        {
            int i = 0;
            foreach (var item in source)
            {
                if (i == index)
                {
                    return item;
                }

                i++;
            }

            return default;
        }

        private IEnumerable<IEnumerable<T>> GetWords(Node<T> node)
        {
            if (node == null)
            {
                yield break;
            }

            foreach (var edge in node.Edges)
            {
                yield return edge.Value;
                foreach (var value in GetWords(edge.Next))
                {
                    yield return value;
                }
            }
        }
    }
}
