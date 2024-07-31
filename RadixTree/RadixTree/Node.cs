namespace RadixTreeStructure
{
    public class Node
    {
        public Node()
        {
            Label = "";
            ChildrenNodes = new List<Node>();
        }

        public Node(string value)
        {
            Label = value;
            ChildrenNodes = new List<Node>();
        }

        internal string Label { get; set; }

        internal List<Node> ChildrenNodes { get; }
    }
}