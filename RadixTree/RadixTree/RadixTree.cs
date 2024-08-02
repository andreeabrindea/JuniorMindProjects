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

        public void Add(string word) => this.Add(root, word);

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
                    if (mismatchIndex > 0)
                    {
                        word = word[mismatchIndex..];
                    }

                    if (edge.Next != null)
                    {
                        queue.Enqueue(edge.Next);
                    }
                }
            }

            return word == string.Empty;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this.GetWords(this.root, string.Empty).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void Add(Node node, string word)
        {
            if (node == null)
            {
                return;
            }

            var queue = new Queue<Node>();
            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                foreach (var edge in currentNode.Edges)
                {
                    int mismatchIndex = GetFirstMismatchLetterIndex(word, edge.Value);
                    if (mismatchIndex > 0 && edge.Value.Length > mismatchIndex)
                    {
                        currentNode.AddEdge(edge.Value[..mismatchIndex], edge.Next);
                        currentNode.AddEdge(edge.Value[mismatchIndex..], new Node(true));
                        edge.Next.AddEdge(word[mismatchIndex..], new Node(true));
                        currentNode.Edges.Remove(edge);
                        return;
                    }

                    if (mismatchIndex > 0)
                    {
                        currentNode.AddEdge(word[mismatchIndex..], new Node(true));
                        return;
                    }

                    if (edge.Next != null)
                    {
                        queue.Enqueue(edge.Next);
                    }
                }
            }

            root.AddEdge(word, new Node(true));
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
