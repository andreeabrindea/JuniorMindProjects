namespace BTree;

public class BTree<T>
{
    private Node<T> root;
    
    public BTree(int degree=3)
    {
        this.root = new Node<T>(degree);
    }
}



