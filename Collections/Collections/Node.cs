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

    public T Data { get; internal set; }

    public Node<T> Next { get; internal set; }

    public Node<T> Previous { get; internal set; }

    public CircularDoublyLinkedList<T> Instance { get; internal set; }
}

#pragma warning restore CA2227