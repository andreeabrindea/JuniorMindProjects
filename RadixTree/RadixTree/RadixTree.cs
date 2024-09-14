﻿using System.Collections;

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

        public IEnumerator<IEnumerable<T>> GetEnumerator() => GetEdges(this.root).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(IEnumerable<T> enumeration) => Add(root, enumeration);

        public bool Search(IEnumerable<T> enumeration) => Search(root, enumeration);

        public bool Remove(IEnumerable<T> enumeration) => Remove(root, enumeration);

        private void Add(Node<T> node, IEnumerable<T> enumeration)
        {
            if (Search(node, enumeration))
            {
                return;
            }

            foreach (var edge in node.Edges)
            {
                int mismatchIndex = GetFirstMismatchLetterIndex(enumeration, edge.Value);
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

                    if (mismatchIndex < Count(enumeration))
                    {
                        edge.Next.AddEdge(Slice(enumeration, mismatchIndex, Count(enumeration)), new Node<T>(true));
                    }

                    Edge<T> splitEdge = edge.Next.GetEdge(suffixEdge);
                    splitEdge.Next.CopyEdges(previousEdges);

                    return;
                }

                if (Count(enumeration) - mismatchIndex > 0)
                {
                    edge.Next.AddEdge(Slice(enumeration, mismatchIndex, Count(enumeration)), new Node<T>(true));
                    return;
                }

                Add(edge.Next, enumeration);
            }

            root.AddEdge(enumeration, new Node<T>(true));
        }

        private bool Search(Node<T> node, IEnumerable<T> enumeration)
        {
            foreach (var edge in node.Edges)
            {
                int mismatchIndex = GetFirstMismatchLetterIndex(enumeration, edge.Value);
                if (mismatchIndex == Count(edge.Value) && Count(edge.Value) == Count(enumeration))
                {
                    return true;
                }

                enumeration = Slice(enumeration, mismatchIndex, Count(enumeration));
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
                int mismatchIndex = GetFirstMismatchLetterIndex(enumeration, edge.Value);
                if (mismatchIndex == Count(edge.Value) && mismatchIndex == Count(enumeration))
                {
                    node.Edges.Remove(edge);
                    return true;
                }

                if (mismatchIndex > 0 && node.IsLeaf)
                {
                    node.Edges.Remove(edge);
                    return true;
                }

                enumeration = Slice(enumeration, mismatchIndex, Count(enumeration));
                Remove(edge.Next, enumeration);
            }

            return false;
        }

        private int GetFirstMismatchLetterIndex(IEnumerable<T> enumeration, IEnumerable<T> edgeWord)
        {
            int length = Math.Min(Count(enumeration), Count(edgeWord));
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

        private int Count(IEnumerable<T> source)
        {
            int index = 0;
            foreach (var unused in source)
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
