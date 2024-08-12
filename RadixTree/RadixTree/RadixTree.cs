using System.Collections;

namespace RadixTreeStructure
{
    public class RadixTree : IEnumerable<string>
    {
        private readonly Node root;

        public RadixTree()
        {
            this.root = new Node(false);
        }

        public void Add(string word)
        {
            if (root.Edges.Count == 0)
            {
                root.AddEdge(word, new Node(true));
                return;
            }

            foreach (var edge in root.Edges)
            {
                int mismatchIndex = GetFirstMismatchLetterIndex(word, edge.Value);
                if (edge.Value.Length > mismatchIndex)
                {
                    string commonPrefix = edge.Value[..mismatchIndex];
                    string suffixEdge = edge.Value[mismatchIndex..];
                    edge.Value = commonPrefix;
                    root.AddEdge(suffixEdge, new Node(true));

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
            }

            root.AddEdge(word, new Node(true));
        }

        public bool Search(string word)
        {
            if (root == null)
            {
                return false;
            }

            var queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                foreach (var edge in node.Edges)
                {
                    int mismatchIndex = GetFirstMismatchLetterIndex(word, edge.Value);
                    if (mismatchIndex == edge.Value.Length && edge.Value.Length == word.Length)
                    {
                        return true;
                    }

                    word = word[mismatchIndex..];

                    if (edge.Next != null)
                    {
                        queue.Enqueue(edge.Next);
                    }
                }
            }

            return false;
        }

        public bool Remove(string word)
        {
            if (!Search(word))
            {
                return false;
            }

            var queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
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

                    if (edge.Next != null)
                    {
                        queue.Enqueue(edge.Next);
                    }
                }
            }

            return false;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this.GetWords(this.root, string.Empty).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
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
