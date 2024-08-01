using System;
using System.Collections;
using System.Collections.Generic;

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
            throw NotImplementedException();
        }

        public bool Search(string word)
        {
            throw NotImplementedException();
        }

        public void Delete(string word)
        {
            throw NotImplementedException();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this.GetWords(this.root, string.Empty).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private Node Delete(Node currentNode, string word)
        {
            throw NotImplementedException();
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

            return -1;
        }
    }
}
