using System.Collections;

namespace RadixTreeStructure
{
    public class RadixTree : IEnumerable<string>
    {
        private readonly Node root;

        public RadixTree()
        {
            root = new Node(string.Empty);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this.Traverse(root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(string word)
        {
            this.Insert(word, root);
        }

        public void Delete(string label)
        {
            this.Delete(label, root);
        }

        public bool Search(string word)
        {
            return this.Search(word, root);
        }

        private void Insert(string wordPart, Node currentNode)
        {
            var matches = this.GetNumberOfMatchingConsecutiveCharacters(wordPart, currentNode);

            if (this.CanProcessCurrentNode(matches, wordPart, currentNode))
            {
                bool inserted = false;
                var newWordPart = wordPart.Substring(matches, wordPart.Length - matches);
                foreach (var child in currentNode.ChildrenNodes)
                {
                    if (child.Label.StartsWith(newWordPart[0]))
                    {
                        inserted = true;
                        Insert(newWordPart, child);
                    }
                }

                if (!inserted)
                {
                    currentNode.ChildrenNodes.Add(new Node(newWordPart));
                }
            }
            else if (matches < wordPart.Length)
            {
                string commonRoot = wordPart[..matches];
                string branchPreviousLabel = currentNode.Label[matches..];
                string branchNewLabel = wordPart[matches..];

                currentNode.Label = commonRoot;

                var newNodePreviousLabel = new Node(branchPreviousLabel);
                newNodePreviousLabel.ChildrenNodes.AddRange(currentNode.ChildrenNodes);

                currentNode.ChildrenNodes.Clear();
                currentNode.ChildrenNodes.Add(newNodePreviousLabel);

                var newNodeNewLabel = new Node(branchNewLabel);
                currentNode.ChildrenNodes.Add(newNodeNewLabel);
            }
            else if (matches > currentNode.Label.Length)
            {
                string newNodeLabel = currentNode.Label.Substring(currentNode.Label.Length, wordPart.Length);
                var newNode = new Node(newNodeLabel);
                currentNode.ChildrenNodes.Add(newNode);
            }
        }

        private bool CanProcessCurrentNode(int matches, string wordPart, Node currentNode)
        {
            if (matches == 0 || currentNode == root)
            {
                return true;
            }

            return matches > 0 && matches < wordPart.Length && matches >= currentNode.Label.Length;
        }

        private int GetNumberOfMatchingConsecutiveCharacters(string word, Node currentNode)
        {
            int minLength = currentNode.Label.Length >= word.Length ? word.Length : currentNode.Label.Length;

            int matches = 0;
            if (minLength <= 0)
            {
                return matches;
            }

            for (int i = 0; i < minLength; i++)
            {
                if (word[i] == currentNode.Label[i])
                {
                    matches++;
                }
                else
                {
                    break;
                }
            }

            return matches;
        }

        private bool Search(string wordPart, Node currentNode)
        {
            var matches = GetNumberOfMatchingConsecutiveCharacters(wordPart, currentNode);

            if (CanProcessCurrentNode(matches, wordPart, currentNode))
            {
                var newLabel = wordPart[matches..];
                foreach (var child in currentNode.ChildrenNodes)
                {
                    if (child.Label.StartsWith(newLabel[0]))
                    {
                        return this.Search(newLabel, child);
                    }
                }

                return false;
            }

            return matches == currentNode.Label.Length;
        }

        private string FindSuccessor(string word)
        {
            return this.FindSuccessor(word, root, string.Empty);
        }

        private string FindSuccessor(string wordPart, Node currentNode, string carry)
        {
            var matches = GetNumberOfMatchingConsecutiveCharacters(wordPart, currentNode);

            if (CanProcessCurrentNode(matches, wordPart, currentNode))
            {
                var newLabel = wordPart.Substring(matches, wordPart.Length - matches);
                foreach (var child in currentNode.ChildrenNodes)
                {
                    if (child.Label.StartsWith(newLabel[0]))
                    {
                        return this.FindSuccessor(newLabel, child, carry + currentNode.Label);
                    }
                }

                return currentNode.Label;
            }

            if (matches < currentNode.Label.Length)
            {
                return carry + currentNode.Label;
            }

            if (matches == currentNode.Label.Length)
            {
                carry += currentNode.Label;

                int min = int.MaxValue;
                int index = -1;
                for (int i = 0; i < currentNode.ChildrenNodes.Count; i++)
                {
                    if (currentNode.ChildrenNodes[i].Label.Length < min)
                    {
                        min = currentNode.ChildrenNodes[i].Label.Length;
                        index = i;
                    }
                }

                if (index > -1)
                {
                    return carry + currentNode.ChildrenNodes[index].Label;
                }

                return carry;
            }

            return string.Empty;
        }

        private string FindPredecessor(string word)
        {
            return this.FindPredecessor(word, root, string.Empty);
        }

        private string FindPredecessor(string wordPart, Node currentNode, string carry)
        {
            var matches = this.GetNumberOfMatchingConsecutiveCharacters(wordPart, currentNode);

            if (CanProcessCurrentNode(matches, wordPart, currentNode))
            {
                var newLabel = wordPart.Substring(matches, wordPart.Length - matches);
                foreach (var child in currentNode.ChildrenNodes)
                {
                    if (child.Label.StartsWith(newLabel[0].ToString()))
                    {
                        return this.FindPredecessor(newLabel, child, carry + currentNode.Label);
                    }
                }

                return carry + currentNode.Label;
            }

            if (matches == currentNode.Label.Length)
            {
                return carry + currentNode.Label;
            }

            return string.Empty;
        }

        private void Delete(string wordPart, Node currentNode)
        {
            var matches = this.GetNumberOfMatchingConsecutiveCharacters(wordPart, currentNode);
            if (!CanProcessCurrentNode(matches, wordPart, currentNode))
            {
                return;
            }

            var newLabel = wordPart[matches..];
            foreach (var child in currentNode.ChildrenNodes)
            {
                if (child.Label.StartsWith(newLabel[0]))
                {
                    if (newLabel == child.Label && child.ChildrenNodes.Count == 0)
                    {
                            currentNode.ChildrenNodes.Remove(child);
                            return;
                    }

                    this.Delete(newLabel, child);
                }
            }
        }

        private IEnumerable<string> Traverse(Node node)
        {
            if (node != root)
            {
                yield return node.Label;
            }

            foreach (var child in node.ChildrenNodes)
            {
                foreach (var word in Traverse(child))
                {
                    yield return node.Label + word;
                }
            }
        }
    }
}
