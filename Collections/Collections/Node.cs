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

    internal Node<T> Next { get; set; }

    internal Node<T> Previous { get; set; }

    internal CircularDoublyLinkedList<T> Instance { get;  set; }
}

#pragma warning restore CA2227