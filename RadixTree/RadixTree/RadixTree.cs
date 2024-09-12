using System.Collections;
using System.Security.Cryptography;

namespace RadixTreeStructure
{
    public class RadixTree<T> : IEnumerable<T>
        where T : IEnumerable
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
            return GetWords(this.root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerable<T> GetWords(Node<T> node)
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

        private void Add(Node<T> node, T word)
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
                    T commonPrefix = Slice(edge.Value, 0, mismatchIndex);
                    T suffixEdge = Slice(edge.Value, mismatchIndex, Count(edge.Value));
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

        private bool Search(Node<T> node, T word)
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

        private bool Remove(Node<T> node, T word)
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

        private int GetFirstMismatchLetterIndex(T word, T edgeWord)
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

        private T Slice(T source, int start, int end)
        {
            if (source is string s)
            {
                string result = s.Substring(start, end - start);
                return (T)(object)result;
            }

            if (source is Array array)
            {
                Type elementType = array.GetType().GetElementType();
                Array newArray = Array.CreateInstance(elementType, end - start);
                Array.Copy(array, start, newArray, 0, end - start);
                return (T)(object)newArray;
            }

            if (typeof(T) == typeof(List<char>))
            {
                List<char> sourceList = (List<char>)(object)source;
                List<char> result = sourceList.GetRange(start, end - start);
                return (T)(object)result;
            }

            throw new NotSupportedException($"Type {typeof(T)} is not supported for Slice.");
        }

        private int Count(T source)
        {
            int index = 0;
            foreach (var item in source)
            {
                index++;
            }

            return index;
        }

        private object GetElementAtIndex(T source, int index)
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
    }
}
