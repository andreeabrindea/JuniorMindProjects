using System.Collections;

namespace RadixTreeStructure
{
    public class RadixTree<T> : IEnumerable<T>
        where T : IList, new()
    {
        private readonly Node<T> root;

        public RadixTree()
        {
            this.root = new Node<T>(false);
        }

        public void Add(T item) => Add(root, item);

        public bool Search(T item) => Search(root, item);

        public bool Remove(T item) => Remove(root, item);

        public IEnumerator<T> GetEnumerator()
        {
            return GetWords(this.root, new T()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerable<T> GetWords(Node<T> node, T prefix)
        {
            if (node.IsLeaf)
            {
                yield return prefix;
            }

            foreach (var edge in node.Edges)
            {
                foreach (var character in edge.Value)
                {
                    prefix.Add(character);
                }

                foreach (var word in this.GetWords(edge.Next, prefix))
                {
                    yield return word;
                }
            }
        }

        private void Add(Node<T> node, T word)
        {
            if (Search(node, word))
            {
                return;
            }

            foreach (var edge in node.Edges)
            {
                int mismatchIndex = GetFirstMismatchLetterIndex(word, edge.Value);
                if (edge.Value.Count > mismatchIndex)
                {
                    T commonPrefix = Slice(edge.Value, 0, mismatchIndex);

                    T suffixEdge = Slice(edge.Value, mismatchIndex, word.Count);

                    edge.Value = commonPrefix;
                    node.AddEdge(suffixEdge, new Node<T>(true));

                    Node<T> newNode = new(false);
                    edge.Next = newNode;
                    newNode.AddEdge(Slice(word, mismatchIndex, word.Count), new Node<T>(true));
                    return;
                }

                if (word.Count - mismatchIndex > 0)
                {
                    edge.Next.AddEdge(Slice(word, mismatchIndex, word.Count), new Node<T>(true));
                    return;
                }

                Add(edge.Next, word);
            }

            root.AddEdge(word, new Node<T>(true));
        }

        private bool Search(Node<T> node, T word)
        {
            foreach (var edge in node.Edges)
            {
                int mismatchIndex = GetFirstMismatchLetterIndex(word, edge.Value);
                if (mismatchIndex == edge.Value.Count && edge.Value.Count == word.Count)
                {
                    return true;
                }

                word = Slice(word, mismatchIndex, word.Count);
                Search(edge.Next, word);
            }

            return false;
        }

        private bool Remove(Node<T> node, T word)
        {
            if (!Search(node, word))
            {
                return false;
            }

            foreach (var edge in node.Edges)
            {
                int mismatchIndex = GetFirstMismatchLetterIndex(word, edge.Value);
                if (mismatchIndex == edge.Value.Count && mismatchIndex == word.Count)
                {
                    node.Edges.Remove(edge);
                    return true;
                }

                if (mismatchIndex > 0 && node.IsLeaf)
                {
                    node.Edges.Remove(edge);
                    return true;
                }

                word = Slice(word, mismatchIndex, word.Count);
                Remove(edge.Next, word);
            }

            return false;
        }

        private int GetFirstMismatchLetterIndex(T word, T edgeWord)
        {
            int length = Math.Min(word.Count, edgeWord.Count);
            for (int i = 0; i < length; i++)
            {
                if (!word[i].Equals(edgeWord[i]))
                {
                    return i;
                }
            }

            return length;
        }

        private T Slice(T source, int start, int end)
        {
            var result = new T();
            for (int i = start; i < end; i++)
            {
                result.Add(source[i]);
            }

            return result;
        }
    }
}
