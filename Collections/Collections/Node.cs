namespace Collections;

public class Node<T>
{
    public Node()
    {
    }

    public Node(T data)
    {
        Data = data;
    }

    public T Data { get; set; }

    public Node<T> Next { get; set; }

    public Node<T> Previous { get; set; }
}