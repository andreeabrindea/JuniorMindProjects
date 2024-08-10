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
            foreach (var edge in root.Edges)
            {
                int mismatchIndex = GetFirstMismatchLetterIndex(word, edge.Value);
                if (mismatchIndex > 0 && mismatchIndex == edge.Value.Length)
                {
                    root.AddEdge(word[mismatchIndex..], new Node(true));
                    return;
                }

                if (mismatchIndex > 0 && mismatchIndex == word.Length)
                {
                  root.AddEdge(edge.Value[..mismatchIndex], edge.Next ?? new Node(true));
                  root.AddEdge(edge.Value[mismatchIndex..], new Node(true));
                  edge.Next.AddEdge(word[mismatchIndex..], new Node(true));
                  root.Edges.Remove(edge);
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
