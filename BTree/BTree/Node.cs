namespace BTree;

public class Node<T>
{
    private T[] keys;
    private Node<T>[] children;

    public Node(int degree)
    {
        this.keys = new T[degree];
        this.children = new Node<T>[degree];
    }
}