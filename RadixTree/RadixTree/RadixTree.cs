using System.Collections;

namespace RadixTreeStructure
{
    public class RadixTree<T> : IEnumerable<T>
    {
        private readonly Node root;

        public RadixTree()
        {
            this.root = new Node(false);
        }

        public void Add(string word) => Add(root, word);

        public bool Search(string word) => Search(root, word);

        public bool Remove(string word) => Remove(root, word);

        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)this.GetWords(this.root, string.Empty).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void Add(Node node, string word)
        {
            if (Search(word))
            {
                return;
            }

            foreach (var edge in node.Edges)
            {
                int mismatchIndex = GetFirstMismatchLetterIndex(word, edge.Value);
                if (edge.Value.Length > mismatchIndex)
                {
                    string commonPrefix = edge.Value[..mismatchIndex];
                    string suffixEdge = edge.Value[mismatchIndex..];
                    edge.Value = commonPrefix;
                    node.AddEdge(suffixEdge, new Node(true));

                    Node newNode = new(false);
                    edge.Next = newNode;
                    newNode.AddEdge(word[mismatchIndex..], new Node(true));
                    return;
                }

                if (word.Length - mismatchIndex > 0)
                {
                    edge.Next.AddEdge(word[mismatchIndex..], new Node(true));
                    return;
                }

                Add(edge.Next, word);
            }

            root.AddEdge(word, new Node(true));
        }

        private bool Search(Node node, string word)
        {
            foreach (var edge in node.Edges)
            {
                int mismatchIndex = GetFirstMismatchLetterIndex(word, edge.Value);
                if (mismatchIndex == edge.Value.Length && edge.Value.Length == word.Length)
                {
                    return true;
                }

                word = word[mismatchIndex..];
                Search(edge.Next, word);
            }

            return false;
        }

        private bool Remove(Node node, string word)
        {
            if (!Search(word))
            {
                return false;
            }

            foreach (var edge in node.Edges)
            {
                int mismatchIndex = GetFirstMismatchLetterIndex(word, edge.Value);
                if (mismatchIndex == edge.Value.Length && mismatchIndex == word.Length)
                {
                    node.Edges.Remove(edge);
                    return true;
                }

                if (mismatchIndex > 0 && node.IsLeaf)
                {
                    node.Edges.Remove(edge);
                    return true;
                }

                word = word[mismatchIndex..];
                Remove(edge.Next, word);
            }

            return false;
        }

        private IEnumerable<string> GetWords(Node node, string prefix)
        {
            if (node.IsLeaf)
            {
                yield return prefix;
            }

            foreach (var edge in node.Edges)
            {
                foreach (var word in this.GetWords(edge.Next, prefix + edge.Value))
                {
                    yield return word;
                }
            }
        }

        private int GetFirstMismatchLetterIndex(string word, string edgeWord)
        {
            int length = Math.Min(word.Length, edgeWord.Length);
            for (int i = 0; i < length; i++)
            {
                if (word[i] != edgeWord[i])
                {
                    return i;
                }
            }

            return length;
        }
    }
}
