#pragma warning disable CA2227
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

    public CircularDoublyLinkedList<T> Instance { get; set; }
}

#pragma warning restore CA2227