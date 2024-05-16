using System.Collections;

namespace DataStructures;

public class ObjectArrayEnumerator : IEnumerator
{
    private readonly object[] arrayOfObjects;
    private int index;

    public ObjectArrayEnumerator(object[] array, int index = -1)
    {
        this.arrayOfObjects = array;
        this.index = index;
    }

    public object Current => arrayOfObjects[index];

    public bool MoveNext()
    {
        index++;
        return index < arrayOfObjects.Length;
    }

    public void Reset()
    {
        index = -1;
    }
}