﻿using System.Collections;

namespace RadixTree;

public class RadixTree : IEnumerable<string>
{
    private readonly Node root;

    public RadixTree()
    {
        this.root = new Node(false);
    }

    public void Add(string word)
    {
        Node currentNode = this.root;
        int currentIndex = 0;

        while (currentIndex < word.Length)
        {
            char prefix = word[currentIndex];
            Edge currentEdge = currentNode.GetEdgeStringValue(prefix);
            string remainingSubstring = word[currentIndex..];

            if (currentEdge == null)
            {
                currentNode.Edges[prefix] = new Edge(remainingSubstring);
                break;
            }

            int mismatchLetterIndex = this.GetFirstMismatchLetterIndex(remainingSubstring, currentEdge.Value);
            if (mismatchLetterIndex == -1)
            {
                if (remainingSubstring.Length == currentEdge.Value.Length)
                {
                    currentEdge.Next.IsLeaf = true;
                    break;
                }

                if (remainingSubstring.Length < currentEdge.Value.Length)
                {
                    string suffix = currentEdge.Value.Substring(remainingSubstring.Length);
                    currentEdge.Value = remainingSubstring;
                    Node newNext = new Node(true);
                    Node afterNewNext = currentEdge.Next;
                    currentEdge.Next = newNext;
                    newNext.AddEdge(suffix, afterNewNext);
                    break;
                }

                mismatchLetterIndex = currentEdge.Value.Length;
            }
            else
            {
                string suffix = currentEdge.Value.Substring(mismatchLetterIndex);
                currentEdge.Value = currentEdge.Value[..mismatchLetterIndex];
                Node prevNext = currentEdge.Next;
                currentEdge.Next = new Node(false);
                currentEdge.Next.AddEdge(suffix, prevNext);
            }

            currentNode = currentEdge.Next;
            currentIndex += mismatchLetterIndex;
        }
    }

    public bool Search(string word)
    {
        Node currentNode = root;
        int currentIndex = 0;
        while (currentIndex < word.Length)
        {
            char prefix = word[currentIndex];
            Edge edge = currentNode.GetEdgeStringValue(prefix);
            if (edge == null)
            {
                return false;
            }

            string remainingSubstring = word[currentIndex..];
            if (!remainingSubstring.StartsWith(edge.Value))
            {
                return false;
            }

            currentIndex += edge.Value.Length;
            currentNode = edge.Next;
        }

        return currentNode.IsLeaf;
    }

    public void Delete(string word) {
        Delete(root, word);
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
        if (word.Length == 0)
        {
            if (currentNode.Edges.Count == 0 && currentNode != root)
            {
                return null;
            }

            currentNode.IsLeaf = false;
            return currentNode;
        }

        char prefix = word[0];
        Edge edge = currentNode.GetEdgeStringValue(prefix);

        if (edge == null || !word.StartsWith(edge.Value))
        {
            return currentNode;
        }

        Node deleted = Delete(edge.Next, word[edge.Value.Length..]);
        if (deleted == null)
        {
            currentNode.Edges.Remove(prefix);
            if (currentNode.NoOfEdges() == 0 && !currentNode.IsLeaf && currentNode != root)
            {
                return null;
            }
        }
        else if (deleted.NoOfEdges() == 1 && !deleted.IsLeaf)
        {
            currentNode.Edges.Remove(prefix);
            foreach (Edge afterDeleted in deleted.Edges.Values)
            {
                currentNode.AddEdge(edge.Value + afterDeleted.Value, afterDeleted.Next);
            }
        }

        return currentNode;
    }

    private IEnumerable<string> GetWords(Node node, string prefix)
    {
        if (node.IsLeaf)
        {
            yield return prefix;
        }

        foreach (var edge in node.Edges.Values)
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
        for (int i = 1; i < length; i++)
        {
            if (word[i] != edgeWord[i])
            {
                return i;
            }
        }

        return -1;
    }
}