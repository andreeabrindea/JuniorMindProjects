using Collections;

CircularDoublyLinkedList<int> list = new();
list.Add(1);
list.Add(2);
list.Add(3);
list.Add(4);

var previousNode = list.FindLast(2);
Console.WriteLine(previousNode.Data);
var node = new Node<int>(9);
list.AddBefore(node, previousNode);

foreach (var elem in list)
{
    Console.WriteLine(elem);
}