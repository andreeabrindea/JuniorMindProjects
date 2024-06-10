using Collections;

CircularDoublyLinkedList<int> list = new();
list.Add(1);
list.Add(2);
list.Add(3);

list.Remove(2);
foreach (var elem in list)
{
    Console.WriteLine(elem);
}
