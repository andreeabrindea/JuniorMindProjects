using System.Collections;

namespace RadixTree;

public class RadixTree : IEnumerable<string>
{
    private Node root;

    public RadixTree()
    {
        root = new Node(false);
    }

    public void Add(String word) {
        Node current = root;
        int currIndex = 0;

        while (currIndex < word.Length)
        {
            char transitionChar = word[currIndex];
            Edge currentEdge = current.GetTransition(transitionChar);
            string currStr = word.Substring(currIndex);

            if (currentEdge == null)
            {
                current.Edges[transitionChar] = new Edge(currStr);
                break;
            }

            int splitIndex = GetFirstMismatchLetter(currStr, currentEdge.Label);
            if (splitIndex == -1)
            {
                if (currStr.Length == currentEdge.Label.Length)
                {
                    currentEdge.Next.IsLeaf = true;
                    break;
                }

                if (currStr.Length < currentEdge.Label.Length)
                {
                    string suffix = currentEdge.Label.Substring(currStr.Length);
                    currentEdge.Label = currStr;
                    Node newNext = new Node(true);
                    Node afterNewNext = currentEdge.Next;
                    currentEdge.Next = newNext;
                    newNext.AddEdge(suffix, afterNewNext);
                    break;
                }

                splitIndex = currentEdge.Label.Length;
            }
            else
            {
                string suffix = currentEdge.Label.Substring(splitIndex);
                currentEdge.Label = currentEdge.Label.Substring(0, splitIndex);
                Node prevNext = currentEdge.Next;
                currentEdge.Next = new Node(false);
                currentEdge.Next.AddEdge(suffix, prevNext);
            }

            current = currentEdge.Next;
            currIndex += splitIndex;
        }
    }

    public IEnumerator<string> GetEnumerator()
    {
        return GetWords(root, string.Empty).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private IEnumerable<string> GetWords(Node node, string prefix)
    {
        if (node.IsLeaf)
        {
            yield return prefix;
        }

        foreach (var edge in node.Edges.Values)
        {
            foreach (var word in GetWords(edge.Next, prefix + edge.Label))
            {
                yield return word;
            }
        }
    }


    private int GetFirstMismatchLetter(string word, string edgeWord)
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